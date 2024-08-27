using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.RemoveToken;

public record RemoveUserTokenCommand(long UserId, long TokenId) : IBaseCommand<string>;


internal class RemoveUserTokenCommandHandler : IBaseCommandHandler<RemoveUserTokenCommand, string>
{
    private readonly IUserRepository _repository;

    public RemoveUserTokenCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<string>> Handle(RemoveUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult<string>.NotFound();
        }
        var hashJwtToken = user.RemoveToken(request.TokenId);
        await _repository.Save();
        return OperationResult<string>.Success(hashJwtToken);
    }
}

