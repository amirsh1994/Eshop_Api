using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetById;

internal class GetSellerInventoryByIdQueryHandler : IBaseQueryHandler<GetSellerInventoryByIdQuery, InventoryDto?>
{
    private readonly DapperContext _dapperContext;

    public GetSellerInventoryByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<InventoryDto?> Handle(GetSellerInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = @$"SELECT i.Id, i.Count, i.Price, i.ProductId, i.SellerId,i.DiscountPercentage,i.CreationDate ,s.ShopName,p.Title as ProductTitle,p.ImageName as ProductImage
            FROM{_dapperContext.Inventories} i inner join {_dapperContext.Sellers} s on i.SellerId = s.Id
            inner join {_dapperContext.Products} p on i.ProductId = p.Id
            WHERE i.Id = @InventoryId";

        var inventory = await connection.QueryFirstOrDefaultAsync<InventoryDto?>(sql, new { request.InventoryId });
        if (inventory == null)
            return null;
        return inventory;
    }
}