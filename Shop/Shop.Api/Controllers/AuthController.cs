using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;
using UAParser;


namespace Shop.Api.Controllers;


public class AuthController : ApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IConfiguration _configuration;

    public AuthController(IUserFacade userFacade, IConfiguration configuration)
    {
        _userFacade = userFacade;
        _configuration = configuration;
    }
    [HttpPost("login")]
    public async Task<ApiResult<LoginResultDto?>> Login(LoginViewModel viewModel)
    {
        if (ModelState.IsValid == false)
        {
            return new ApiResult<LoginResultDto?>()
            {
                Data = null,
                IsSuccess = false,
                MetaData = new()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = JoinErrors()
                }
            };
        }

        var user = await _userFacade.GetUserByPhoneNumber(viewModel.PhoneNumber);

        if (user == null)
            return CommandResult(OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد"));


        if (Sha256Hasher.IsCompare(user.Password, viewModel.Password) == false)////اینجا میاد هشی که تو دیتابیس وجود داره رو با مقداری که تو ویو مدل بهش میدیم مقایسه میکنه اگه یکی باشه میزاره بیایی تو
            return CommandResult(OperationResult<LoginResultDto>.Error("رمز عبور وارد شده اشتباه است"));



        if (user.IsActive == false)
            return CommandResult(OperationResult<LoginResultDto>.Error("حساب کاربری شما غیر فعال هست"));

        var loginResult = await AddTokenAndGenerateJwt(user);
        return CommandResult(loginResult);


    }

    #region  AddTokenAndGenerateJwt

    private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
    {
        var parser = Parser.GetDefault();
        var header = HttpContext.Request.Headers["User-Agent"].ToString();
        var device = "windows";
        if (header!=null)
        {
            var info = parser.Parse(header);
             device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major} {info.OS.Minor} -{info.UA.Family}";
        }
        


        var token = JwtTokenBuilder.BuildToken(user, _configuration);
        var refreshToken = Guid.NewGuid().ToString();
        var hashJwtToken = Sha256Hasher.Hash(token);
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);



        var tokenResult = await _userFacade.AddToken(new AddUserTokenCommand(user.Id, hashJwtToken, hashRefreshToken,
            DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));
        if (tokenResult.Status != OperationResultStatus.Success)
            return OperationResult<LoginResultDto?>.Error();

        return OperationResult<LoginResultDto?>.Success(new LoginResultDto()
        {
            Token = token,
            RefreshToken = refreshToken
        });
    }

    #endregion



    [HttpPost("register")]
    public async Task<ApiResult> Register(RegisterViewModel viewModel)
    {
        if (ModelState.IsValid == false)
        {
            return new ApiResult()
            {
                IsSuccess = false,
                MetaData = new()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = JoinErrors()
                }
            };
        }
        var command = new RegisterUserCommand(new PhoneNumber(viewModel.PhoneNumber), viewModel.Password);//اینجا اومدم توی هندلرش کامند رجیستر پسوورد رو بصورت هش درستش کردم که بصورت هش شده ذخیره بشه 
        var result = await _userFacade.RegisterUser(command);
        return CommandResult(result);
    }


    [HttpPost("RefreshToken")]

    public async Task<ApiResult<LoginResultDto?>> RefreshToken(string refreshToken)
    {
        var result = await _userFacade.GetUserTokenByHashRefreshToken(refreshToken);

        if (result == null)
            return CommandResult(OperationResult<LoginResultDto?>.NotFound());

        if (result.TokenExpireDate > DateTime.Now)
            return CommandResult(OperationResult<LoginResultDto?>.Error("توکن هنوز منقضی نشده است"));

        if (result.RefreshTokenExpireDate < DateTime.Now)
            return CommandResult(OperationResult<LoginResultDto?>.Error("زمان رفرش توکن به پایین خودش رسیده است"));

        var userDto = await _userFacade.GetUserById(result.UserId);

        if (userDto == null)
            return CommandResult(OperationResult<LoginResultDto?>.NotFound());


        await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));
        var loginResult = await AddTokenAndGenerateJwt(userDto);

        return CommandResult(loginResult);
    }


    [HttpDelete("logout")]
    [Authorize]
    public async Task<ApiResult> Logout()
    {
        var token = await this.HttpContext.GetTokenAsync("access_token");

        if (token == null)
            return CommandResult(OperationResult.NotFound());

        var result = await _userFacade.GetUserTokenByJwtToken(token);
        if (result == null)
            return CommandResult(OperationResult.NotFound());

        await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));

        return CommandResult(OperationResult.Success());
    }
}

