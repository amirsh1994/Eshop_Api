﻿using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetByProductId;

public class GetInventoriesByProductIdQueryHandler:IBaseQueryHandler<GetInventoriesByProductIdQuery, List<InventoryDto>>
{
    private readonly DapperContext _dapperContext;

    public GetInventoriesByProductIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<InventoryDto>> Handle(GetInventoriesByProductIdQuery request, CancellationToken cancellationToken)
    {
        var sql = $@"SELECT  i.Id, i.SellerId , i.ProductId ,i.Count , i.Price,i.CreationDate , i.DiscountPercentage , s.ShopName ,p.Title as ProductTitle,p.ImageName as ProductImage
                  FROM {_dapperContext.Inventories} i
                        inner join {_dapperContext.Sellers} s on i.SellerId=s.Id
                        inner join {_dapperContext.Products} p on i.ProductId=p.Id
                  WHERE productId=@ProductId";
        var connection = _dapperContext.CreateConnection();
        var result = await connection.QueryAsync<InventoryDto>(sql, new { ProductId = request.ProductId });
        return result.ToList();
    }
}