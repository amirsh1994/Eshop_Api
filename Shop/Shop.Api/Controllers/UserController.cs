using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.User_Management)]
public class UserController : ApiController
{
    private readonly IUserFacade _userFacade;

    public UserController(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [HttpGet]
    public async Task<ApiResult<UserFilterResult>> GetUsersByFilter([FromQuery] UserFilterParams filterParams)
    {
        var result = await _userFacade.GetUserByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{userId:long}")]

    public async Task<ApiResult<UserDto?>> GetUserById(long userId)
    {
        var result = await _userFacade.GetUserById(userId);
        return QueryResult(result);
    }

    [HttpGet("{phoneNumber}")]
    public async Task<ApiResult<UserDto?>> GetUserByPhoneNumber(string phoneNumber)
    {
        var result = await _userFacade.GetUserByPhoneNumber(phoneNumber);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> CreateUser(CreateUserCommand command)
    {
        var result = await _userFacade.CreateUser(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditUser(EditUserCommand command)
    {
        var result= await _userFacade.EditUser(command);
        return CommandResult(result);
    }

    //[HttpPost("register")]
    //public async Task<ApiResult> RegisterUser(RegisterUserCommand command)
    //{
    //    var result=await _userFacade.RegisterUser(command);
    //    return CommandResult(result);
    //}
}

