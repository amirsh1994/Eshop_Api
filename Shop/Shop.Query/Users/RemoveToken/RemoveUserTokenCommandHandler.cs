using Common.Application;
using Shop.Domain.UserAgg.Repository;
using Shop.Infrastructure;

namespace Shop.Query.Users.RemoveToken;

internal class RemoveUserTokenCommandHandler : IBaseCommandHandler<RemoveUserTokenCommand>
{
    private readonly IUserRepository _repository;
    private readonly ShopContext _dapperContext;

    public RemoveUserTokenCommandHandler(IUserRepository repository, ShopContext dapperContext)
    {
        _repository = repository;
        _dapperContext = dapperContext;
    }

    public async Task<OperationResult> Handle(RemoveUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult.NotFound();
        }
        user.RemoveToken(request.TokenId);
        await _repository.Save();
        return OperationResult.Success();
    }
}