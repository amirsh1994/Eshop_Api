using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.OrderAgg.ValueObjects;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.Orders.CheckOut;

public class CheckOutOrderCommandHandler:IBaseCommandHandler<CheckOutOrderCommand>
{
    private readonly IOrderRepository _repository;
    private readonly IShippingMethodRepository _shippingMethodRepository;

    public CheckOutOrderCommandHandler(IOrderRepository repository, IShippingMethodRepository shippingMethodRepository)
    {
        _repository = repository;
        _shippingMethodRepository = shippingMethodRepository;
    }

    public async Task<OperationResult> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
    {
        var currentPendingOrder =await _repository.GetCurrentUserOrder(request.UserId);
        if (currentPendingOrder==null)
        {
            return OperationResult.NotFound();
        }

        var shippingMethode = await _shippingMethodRepository.GetAsync(request.ShippingMethodeId);
        if(shippingMethode==null)
            return OperationResult.Error();
        var address = new OrderAddress(request.Shire, request.City, request.PostalCode,request.PostalAddress, request.PhoneNumber, request.Family, request.NationalCode, request.Name);
        currentPendingOrder.CheckOut(address,new OrderShippingMethod(shippingMethode.Title,shippingMethode.Cost));
        await _repository.Save();
        return OperationResult.Success();
    }
}