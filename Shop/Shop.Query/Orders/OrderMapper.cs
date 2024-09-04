using Dapper;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders;

internal static class OrderMapper
{
    public static OrderDto Map(this Order o)
    {
        
        return new OrderDto()
        {
            CreationDate = o.CreationDate,
            Id = o.Id,
            Status = o.Status,
            Address = o.Address,
            Discount = o.Discount,
            Items = new (),
            LastUpdate = o.LastUpdate,
            Methode = o.Methode,
            UserFullName = "",
            UserId = o.UserId,
        };
    }

    public static async Task<List<OrderItemDto>> GetOrderItem(this OrderDto orderDto,DapperContext dapperContext)
    {
        using var connection = dapperContext.CreateConnection();
        var sql = @$"SELECT s.ShopName,o.OrderId,o.InventoryId,o.Count,o.Price,o.Id,
                    p.Title as ProductTitle ,p.Slug as ProductSlug ,p.ImageName as ProductImageName
         FROM{dapperContext.OrderItems} o 
         
         Inner Join {dapperContext.Inventories} i on o.InventoryId=i.Id 
         Inner Join  {dapperContext.Products} p on i.productId=p.Id
         Inner Join  {dapperContext.Sellers} s on i.sellerId=s.Id
         where o.OrderId=@orderId";
        var result = await connection.QueryAsync<OrderItemDto>(sql, new {orderId=orderDto.Id });
        
        return result.ToList();
    }

    public static OrderFilterData MapFilterData(this Order o,ShopContext context)
    {
        var userFullName = context.Users
            .Where(x => x.Id == o.UserId)
            .Select(x => $"{x.Name}{x.Family}")
            .First();
        return new OrderFilterData()
        {
            Status = o.Status,
            Id = o.Id,
            CreationDate = o.CreationDate,
            City = o.Address?.City,
            ShippingType = o.Methode?.ShippingType,
            Shire = o.Address?.Shire,
            TotalItemCount = o.ItemCount,
            TotalPrice = o.TotalPrice,
            UserFullName = userFullName,
            UserId = o.UserId,
        };
    }
}