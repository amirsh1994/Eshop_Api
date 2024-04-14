using Common.Application;
using MediatR;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Query.Users.Addresses.GetById;
using Shop.Query.Users.Addresses.GetList;
using Shop.Query.Users.DTOs;

namespace Shop.Presentation.Facade.Users.Addresses;

public class UserAddressFacade : IUserAddressFacade
{
    private readonly IMediator _mediator;

    public UserAddressFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddAddress(AddUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditAddress(EditUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public Task<OperationResult> DeleteAddress(DeleteUserAddressCommand command)
    {
      return  _mediator.Send(command);
    }

    public Task<AddressDto?> GetById(long userAddressId)
    {
        return _mediator.Send(new GetUserAddressByIdQuery(userAddressId));
    }

    public Task<List<AddressDto>> GetList(long userId)
    {
       return _mediator.Send(new GetUserAddressListQuery(userId));
    }
}