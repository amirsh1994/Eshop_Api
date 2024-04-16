using Common.Application;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.AddToken;

public class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _service;

    public AddUserTokenCommandHandler(IUserRepository repository, IUserDomainService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<OperationResult> Handle(AddUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);

        if (user == null)
            return OperationResult.NotFound();

        user.AddToken(request.HashJwtToken, request.HashRefreshToken, request.TokenExpireDate, request.RefreshTokenExpireDate, request.Device);
        await _repository.Save();
        return OperationResult.Success();
    }
}