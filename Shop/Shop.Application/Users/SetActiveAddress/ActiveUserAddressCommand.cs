using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.SetActiveAddress;

public record ActiveUserAddressCommand(long UserId, long AddressId) : IBaseCommand;



public class ActiveUserAddressCommandHandler : IBaseCommandHandler<ActiveUserAddressCommand>
{
    private readonly IUserRepository _userRepository;


    public ActiveUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(ActiveUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();
        user.SetActiveAddress(request.AddressId);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}