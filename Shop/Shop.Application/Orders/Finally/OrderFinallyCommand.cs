using Common.Application;
using MediatR;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.Finally;

public record OrderFinallyCommand(long OrderId):IBaseCommand;



public class OrderFinallyCommandHandler:IBaseCommandHandler<OrderFinallyCommand>
{
    private readonly IOrderRepository _orderRepository;

    public OrderFinallyCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async  Task<OperationResult> Handle(OrderFinallyCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetTracking(request.OrderId);
        if (order==null)
        {
            return OperationResult.NotFound("لاشی رفتی ت ادرس ایدی اردر رو تغییر دادی.....!");
        }
        order.Finally();
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}