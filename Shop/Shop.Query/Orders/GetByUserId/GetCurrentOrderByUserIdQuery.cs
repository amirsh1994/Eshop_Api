using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg.Enums;
using Shop.Infrastructure;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetByUserId;

public record GetCurrentOrderByUserIdQuery(long UserId) : IBaseQuery<OrderDto?>;




public class GetCurrentOrderByUserIdQueryHandler:IBaseQueryHandler<GetCurrentOrderByUserIdQuery,OrderDto>
{
    private readonly ShopContext _context;
    private readonly DapperContext _dapperContext;

    public GetCurrentOrderByUserIdQueryHandler(ShopContext context, DapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto?> Handle(GetCurrentOrderByUserIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Status==OrderStatus.Pending, cancellationToken);
        if (order == null)
        {
            return null;
        }
        var orderDto = order.Map();
        orderDto.UserFullName = await _context.Users
            .Where(x => x.Id == orderDto.UserId)
            .Select(x => $"{x.Name} {x.Family}")
            .FirstAsync(cancellationToken);
        orderDto.Items = await orderDto.GetOrderItem(_dapperContext);
        return orderDto;
    }
}



