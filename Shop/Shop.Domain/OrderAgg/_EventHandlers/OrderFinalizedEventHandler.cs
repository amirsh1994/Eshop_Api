using MediatR;
using Shop.Domain.OrderAgg.Events;

namespace Shop.Domain.OrderAgg._EventHandlers;

public class SendSmsOrderFinalizedEventHandler : INotificationHandler<OrderFinalized>
{
    public async Task Handle(OrderFinalized notification, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        Console.WriteLine("------------------------------------------------------------");
    }
}