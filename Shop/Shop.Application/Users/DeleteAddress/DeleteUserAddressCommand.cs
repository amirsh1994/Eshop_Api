using Common.Application;
using FluentValidation;

namespace Shop.Application.Users.DeleteAddress;

public record DeleteUserAddressCommand(long UserId, long AddressId) : IBaseCommand;