using Common.Application;
using Common.Application.SecurityUtil;
using Common.CacheHelper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.ChangePassword;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetByFilter;
using Shop.Query.Users.GetById;
using Shop.Query.Users.GetByPhoneNumber;
using Shop.Query.Users.UserTokens.GetByJwtToken;
using Shop.Query.Users.UserTokens.GetByRefreshToken;

namespace Shop.Presentation.Facade.Users;

public class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _distributedCache;
    public UserFacade(IMediator mediator, IDistributedCache distributedCache)
    {
        _mediator = mediator;
        _distributedCache = distributedCache;
    }


    public async Task<OperationResult> CreateUser(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        return _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
    {

        var result = await _mediator.Send(command);
        if (result.Status!=OperationResultStatus.Success)
        {
            return OperationResult.Error();
        }
        await _distributedCache.RemoveAsync(CacheKeys.UserToken(result.Data));
        return OperationResult.Success();
    }

    public async Task<OperationResult> ChangeUserPassword(ChangeUserPasswordCommand command)
    {
        await _distributedCache.RemoveAsync(CacheKeys.User(command.UserId));
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditUser(EditUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Status == OperationResultStatus.Success)
            await _distributedCache.RemoveAsync(CacheKeys.User(command.UserId));

        return result;
    }

    public async Task<UserDto?> GetUserById(long userId)
    {
        return await _distributedCache.GetOrSet(CacheKeys.User(userId), async ()
            => await _mediator.Send(new GetUserByIdQuery(userId)));
    }

    public Task<UserTokenDto?> GetUserTokenByHashRefreshToken(string refreshToken)
    {
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
        return _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));
    }

    public async Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
    {
        var hashReJwtToken = Sha256Hasher.Hash(jwtToken);
        return await _distributedCache.GetOrSet(CacheKeys.UserToken(hashReJwtToken), async () =>
        {
            return await _mediator.Send(new GetUserTokenByJwtTokenQuery(hashReJwtToken));
        });
    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams));
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }
}