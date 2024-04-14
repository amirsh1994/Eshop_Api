using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetById;

public class GetOrderByIdQueryHandler : IBaseQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly ShopContext _context;
    private readonly DapperContext _dapperContext;

    public GetOrderByIdQueryHandler(ShopContext context, DapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);
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