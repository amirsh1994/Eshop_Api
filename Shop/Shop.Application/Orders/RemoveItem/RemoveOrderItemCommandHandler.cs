using Common.Application;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.RemoveItem;

public class RemoveOrderItemCommandHandler : IBaseCommandHandler<RemoveOrderItemCommand>
{
    private readonly IOrderRepository _repository;

    public RemoveOrderItemCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var currentPendingOrder = await _repository.GetCurrentUserOrder(request.UserId);

        if (currentPendingOrder == null)
        {
            return OperationResult.NotFound();
        }
        currentPendingOrder.RemoveItem(request.ItemId);
        await _repository.Save();
        return OperationResult.Success();

    }
}