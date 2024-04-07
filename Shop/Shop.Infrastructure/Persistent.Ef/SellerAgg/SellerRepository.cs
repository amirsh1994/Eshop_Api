using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Dapper;

namespace Shop.Infrastructure.Persistent.Ef.SellerAgg;

public class SellerRepository:BaseRepository<Seller>,ISellerRepository
{
    private readonly DapperContext _dapperContext;
    
    public SellerRepository(ShopContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }

    //public async Task<InventoryResult?> GetInventoryById(long inventoryId)
    //{
    //    return await Context.Inventories.Where(x => x.Id == inventoryId)
    //        .Select(x => new InventoryResult()
    //        {
    //            Count = x.Count,
    //            Id = x.Id,
    //            Price = x.Price,
    //            ProductId = x.ProductId,
    //            SellerId = x.SellerId
    //        }).FirstOrDefaultAsync();
    //}
    public async Task<InventoryResult?> GetInventoryById(long id)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT * from {_dapperContext.Inventories} Where Id=@InventoryId";
        var p = await connection.QueryFirstOrDefaultAsync<InventoryResult>(sql,new{inventoryId=id});

        return p;
    }
}