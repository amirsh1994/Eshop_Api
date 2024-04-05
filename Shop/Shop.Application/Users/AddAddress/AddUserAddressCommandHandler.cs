using Common.Application;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.AddAddress;

internal class AddUserAddressCommandHandler : IBaseCommandHandler<AddUserAddressCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _service;

    public AddUserAddressCommandHandler(IUserRepository repository, IUserDomainService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<OperationResult> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var userAddress = new UserAddress(request.Shire, request.City, request.PostalCode, request.PostalAddress, request.PhoneNumber, request.Family, request.NationalCode, request.Name);

        user.AddAddress(userAddress);
        await _repository.Save();
        return OperationResult.Success();

    }
}