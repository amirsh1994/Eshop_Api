using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Orders.AddItem;

public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _repository;

    private readonly ISellerRepository _sellerRepository;

    public AddOrderItemCommandHandler(IOrderRepository repository, ISellerRepository sellerRepository)
    {
        _repository = repository;
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var r = await _sellerRepository.GetInventoryById(request.InventoryId);
        if (r == null)
        {
            return OperationResult.NotFound();
        }

        if (r.Count < request.Count)
        {
            return OperationResult.Error("تعداد محصول درخواستی بیشتر از تعداد موجود در انبار می باشد ");
        }

        var order = await _repository.GetCurrentUserOrder(request.UserId);
        if (order == null)
        {
            order = new Order(request.UserId);
            order.AddItem(new OrderItem(request.InventoryId, request.Count, r.Price));
            _repository.Add(order);
        }
        else
        {
            order.AddItem(new OrderItem(request.InventoryId, request.Count, r.Price));

        }
      
        if (ItemCountBiggerThanInventoryCount(r,order))
        {
            return OperationResult.Error("تعداد محصول درخواستی بیشتر از تعداد موجود در انبار می باشد ");
        }
        await _repository.Save();
        return OperationResult.Success();
    }

    private bool ItemCountBiggerThanInventoryCount(InventoryResult inventory, Order order)
    {
        var o = order.Items.First(x => x.InventoryId == inventory.Id);
        if (o.Count > inventory.Count)
            return true;
        return false;
    }
}


//public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
//{
//    var r = await _sellerRepository.GetInventoryById(request.InventoryId);
//    if (r == null)
//    {
//        return OperationResult.NotFound();
//    }

//    if (r.Count < request.Count)
//    {
//        return OperationResult.Error("تعداد محصول درخواستی بیشتر از تعداد موجود در انبار می باشد ");
//    }

//    var order = await _repository.GetCurrentUserOrder(request.UserId) ?? new Order(request.UserId);
//    //if (order == null)
//    //{
//    //    order = new Order(request.UserId);
//    //}
//    order.AddItem(new OrderItem(request.InventoryId, request.Count, r.Price));
//    if (ItemCountBiggerThanInventoryCount(r, order))
//    {
//        return OperationResult.Error("تعداد محصول درخواستی بیشتر از تعداد موجود در انبار می باشد ");
//    }
//    await _repository.Save();
//    return OperationResult.Success();
//}

//private bool ItemCountBiggerThanInventoryCount(InventoryResult inventory, Order order)
//{
//    var o = order.Items.First(x => x.InventoryId == inventory.Id);
//    if (o.Count > inventory.Count)
//        return true;
//    return false;
//}
//}