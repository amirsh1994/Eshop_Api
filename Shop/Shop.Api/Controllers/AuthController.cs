using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.Register;
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
        return CommandResult<LoginResultDto>(loginResult);

    }


    private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
    {
        var parser = Parser.GetDefault();
        var info = parser.Parse(HttpContext.Request.Headers["User-Agent"]);
        var device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major} {info.OS.Minor} -{info.UA.Family}";


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
            Token =token,
            RefreshToken = refreshToken
        });
    }


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
}

