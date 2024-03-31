using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.CheckOut;

public class CheckOutOrderCommandHandler:IBaseCommandHandler<CheckOutOrderCommand>
{
    private readonly IOrderRepository _repository;

    public CheckOutOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
    {
        var currentPendingOrder =await _repository.GetCurrentUserOrder(request.UserId);
        if (currentPendingOrder==null)
        {
            return OperationResult.NotFound();
        }
        currentPendingOrder.CheckOut(new OrderAddress(request.Shire,request.City,request.PostalCode
            ,request.PostalAddress,request.PhoneNumber,request.Family,request.NationalCode,request.Name));
        await _repository.Save();
        return OperationResult.Success();
    }
}