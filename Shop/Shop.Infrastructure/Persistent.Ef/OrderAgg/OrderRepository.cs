using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.OrderAgg;

public class OrderRepository:BaseRepository<Order>,IOrderRepository
{
    public OrderRepository(ShopContext context) : base(context)
    {
    }

    public async Task<Order?> GetCurrentUserOrder(long userId)
    {
        return await Context.Orders.AsTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Status == OrderStatus.Pending);
    }
}