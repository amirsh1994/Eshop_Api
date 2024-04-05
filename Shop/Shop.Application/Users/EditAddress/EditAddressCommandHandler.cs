using Common.Application;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.EditAddress;

public class EditAddressCommandHandler:IBaseCommandHandler<EditUserAddressCommand>
{
    private readonly IUserRepository _repository;

    public EditAddressCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user =await _repository.GetTracking(request.UserId);
        if(user == null) 
            return OperationResult.NotFound();
        var userAddress = new UserAddress(request.Shire, request.City, request.PostalCode, request.PostalAddress,
            request.PhoneNumber, request.Family, request.NationalCode, request.Name);
        user.EditAddress(userAddress,request.Id);
        await _repository.Save();
        return OperationResult.Success();
    }
}