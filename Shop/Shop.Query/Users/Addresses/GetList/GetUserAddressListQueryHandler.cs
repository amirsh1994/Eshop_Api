using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.Addresses.GetList;

internal class GetUserAddressListQueryHandler:IBaseQueryHandler<GetUserAddressListQuery,List<AddressDto>>
{
    private readonly DapperContext _dapperContext;

    public GetUserAddressListQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }


    public async Task<List<AddressDto>> Handle(GetUserAddressListQuery request, CancellationToken cancellationToken)
    {
        var sql = $"select  * from {_dapperContext.UserAddress} where UserId=@UserId";

        using var connection = _dapperContext.CreateConnection();

        var result = await connection.QueryAsync<AddressDto>(sql, new { id = request.UserId });

        return result.ToList();
    }
}