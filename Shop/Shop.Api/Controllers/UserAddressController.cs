using AutoMapper;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.ViewModels.Users;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Application.Users.SetActiveAddress;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers;

[Authorize]
public class UserAddressController : ApiController
{

    private readonly IUserAddressFacade _userAddressFacade;
    private readonly IMapper _mapper;

    public UserAddressController(IUserAddressFacade userAddressFacade, IMapper mapper)
    {
        _userAddressFacade = userAddressFacade;
        _mapper = mapper;
    }

    [HttpGet("{addressId}")]
    public async Task<ApiResult<AddressDto?>> GetUserAddresses(long addressId)
    {
        var result = await _userAddressFacade.GetById(addressId);

        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<List<AddressDto>>> GetList()
    {
        var result = await _userAddressFacade.GetList(User.GetUserId());
        return QueryResult(result);
    }



    [HttpPost]
    public async Task<ApiResult> AdduserAddress(AddUserAddressViewModel viewModel)
    {

        var command = _mapper.Map<AddUserAddressCommand>(viewModel);
        command.UserId = User.GetUserId();
        var result = await _userAddressFacade.AddAddress(command);
        return CommandResult(result);


    }




    [HttpDelete("{addressId}")]
    public async Task<ApiResult> DeleteUserAddress(long addressId)
    {
        var userId = User.GetUserId();
        var result = await _userAddressFacade.DeleteAddress(new DeleteUserAddressCommand(userId, addressId));
        return CommandResult(result);

    }



    [HttpPut]
    public async Task<ApiResult> EditAddress(EditUserAddressViewModel viewModel)
    {
        var command = new EditUserAddressCommand(
            viewModel.Shire, viewModel.City,
            viewModel.PostalCode,
            viewModel.PostalAddress, new PhoneNumber(viewModel.PhoneNumber),
            viewModel.Family, viewModel.Name,
            viewModel.NationalCode, viewModel.Id,
            User.GetUserId()
            );

        var result = await _userAddressFacade.EditAddress(command);
        return CommandResult(result);
    }

    [HttpPut("SetActiveAddress/{addressId:long}")]
    public async Task<ApiResult> ActiveUserAddress(long addressId)
    {
        var result = await _userAddressFacade.SetActiveUserAddress(new ActiveUserAddressCommand(User.GetUserId(), addressId));
        return CommandResult(result);
    }
}

