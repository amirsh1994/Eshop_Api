using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.Register;
using Shop.Presentation.Facade.Users;


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
    public async Task<ApiResult<string?>> Login(LoginViewModel viewModel)
    {
        if (ModelState.IsValid == false)
        {
            return new ApiResult<string?>()
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
            return CommandResult(OperationResult<string>.Error("کاربری با مشخصات وارد شده یافت نشد"));


        if (Sha256Hasher.IsCompare(user.Password, viewModel.Password) == false)
            return CommandResult(OperationResult<string>.Error("رمز عبور وارد شده اشتباه است"));



        if (user.IsActive == false)
            return CommandResult(OperationResult<string>.Error("حساب کاربری شما غیر فعال هست"));


        var token = JwtTokenBuilder.BuildToken(user, _configuration);

        return new ApiResult<string?>()
        {
            Data = token,
            IsSuccess = true,
            MetaData = new()
        };


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
        var command = new RegisterUserCommand(new PhoneNumber(viewModel.PhoneNumber), viewModel.Password);
        var result = await _userFacade.RegisterUser(command);
        return CommandResult(result);
    }
}

