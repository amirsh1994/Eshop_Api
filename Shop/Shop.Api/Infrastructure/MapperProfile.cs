using AutoMapper;
using Shop.Api.ViewModels.Users;
using Shop.Application.ChangePassword;
using Shop.Application.Users.AddAddress;

namespace Shop.Api.Infrastructure;

public class MapperProfile : Profile
{

    public MapperProfile()
    {
        CreateMap<AddUserAddressCommand, AddUserAddressViewModel>().ReverseMap();

        CreateMap<ChangePasswordViewModel, ChangeUserPasswordCommand>().ReverseMap();
    }
}

