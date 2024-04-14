using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.Addresses.GetList;

public record GetUserAddressListQuery(long UserId) : IBaseQuery<List<AddressDto>>;