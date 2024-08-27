using Common.Query;
using Dapper;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetByProductId;

public record GetInventoriesByProductId(long ProductId) : IBaseQuery<List<InventoryDto>>;






public class GetInventoriesByProductIdHandler : IBaseQueryHandler<GetInventoriesByProductId, List<InventoryDto>>
{
    private readonly DapperContext _dapperContext;

    private GetInventoriesByProductIdHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<InventoryDto>> Handle(GetInventoriesByProductId request, CancellationToken cancellationToken)
    {
        var sql = $@"SELECT * FROM {_dapperContext.Inventories} WHERE productId=@ProductId";
        var connection = _dapperContext.CreateConnection();
        var result = await connection.QueryAsync<InventoryDto>(sql, new { ProductId = request.ProductId });
        return result.ToList();
    }
}