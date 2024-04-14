using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.Addresses.GetById;

public class GetUserAddressByIdQueryHandler:IBaseQueryHandler<GetUserAddressByIdQuery, AddressDto?>
{

    private readonly DapperContext _dapperContext;

    public GetUserAddressByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<AddressDto?> Handle(GetUserAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = $"select top 1 * from {_dapperContext.UserAddress} where id=@id";

        using var connection = _dapperContext.CreateConnection();

        var result= await connection.QueryFirstOrDefaultAsync<AddressDto>(sql, new {id = request.AddressId});

        return result;
    }
}