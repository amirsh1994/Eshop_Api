using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Events;
using Shop.Domain.OrderAgg.ValueObjects;

namespace Shop.Domain.OrderAgg;

public class Order:AggregateRoot
{

    private Order()
    {
        
    }

    public Order(long userId)
    {
        UserId = userId;
        Status = OrderStatus.Pending;
        Items=new List<OrderItem>();
    }

    public long UserId { get; private set; }

    public DateTime? LastUpdate { get; set; }

    public OrderStatus Status { get; private set; }

    public List<OrderItem> Items { get; private set; }

    public OrderDiscount? Discount { get; set; }

    public OrderShippingMethod?  Methode { get; set; }

    public OrderAddress? Address { get; private set; }

    public int TotalPrice
    {
        get
        {
            var totalPrice = Items.Sum(x => x.TotalPrice);
            if (Methode != null)
                totalPrice += Methode.ShippingCost;
            if (Discount != null)
                totalPrice -= Discount.DiscountAmount;
            return totalPrice;
        }
    }

    public int ItemCount => Items.Count;

  



    public void AddItem(OrderItem item)
    {
        ChangeOrderGuard();
        var oldItem = Items.FirstOrDefault(x => x.InventoryId == item.InventoryId);
        if (oldItem!=null)
        {
             oldItem.ChangeCount(oldItem.Count+item.Count);
             return;
        }
        Items.Add(item);

    }

    public void RemoveItem(long orderItemId)
    {
        ChangeOrderGuard();
        var orderItem = Items.FirstOrDefault(x => x.Id == orderItemId);
        if (orderItem!=null)
        {
            Items.Remove(orderItem);
            //orderItem.DecreaseCount(orderItem.Count);
        }
       
    }

    public void IncreaseItemCount(long orderItemId,int  count)
    {
        ChangeOrderGuard();
        var currentItem = Items.FirstOrDefault(x => x.Id == orderItemId);
        if (currentItem == null)
        {
            throw new NullOrEmptyDomainDataException("orderItem Was Not Found With Such Id");
        }
        currentItem.IncreaseCount(count);
    }

    public void DecreaseItemCount(long orderItemId, int count)
    {
        ChangeOrderGuard();
        var currentItem = Items.FirstOrDefault(x => x.Id == orderItemId);
        if (currentItem == null)
        {
            throw new NullOrEmptyDomainDataException("orderItem Was Not Found With Such Id");
        }
        currentItem.DecreaseCount(count);
    }

    public void ChangeCountItem(long itemId,int newCount)
    {
        ChangeOrderGuard();
        var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
        if (currentItem == null)
        {
            throw new NullOrEmptyDomainDataException("orderItem Was Not Found With Such Id");
        }
        currentItem.ChangeCount(newCount);
    }

    public void ChangeStatus(OrderStatus status)
    {
        this.Status=status;
        LastUpdate=DateTime.Now;
    }

    public void CheckOut(OrderAddress orderAddress, OrderShippingMethod method)
    {
        ChangeOrderGuard();
        this.Address = orderAddress;
        Methode = method;

    }

    public void ChangeOrderGuard()
    {
        if (Status == OrderStatus.Pending == false)
        {
            throw new InvalidDomainDataException("امکان ویرایش این محصول وجود ندارد");
        }
    }

    public void Finally()
    {
        Status = OrderStatus.Finally;
        LastUpdate=DateTime.Now;
        AddDomainEvent(new OrderFinalized(Id));
    }


}