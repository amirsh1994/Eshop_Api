using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.ValueObjects;

namespace Shop.Domain.OrderAgg;

public class Order:AggregateRoot
{
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

    public ShippingMethode Methode { get; set; }

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

    public OrderAddress? Address { get; private set; }


    private Order()
    {
        
    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);

    }

    public void RemoveItem(long orderItemId)
    {
        var orderItem = Items.FirstOrDefault(x => x.Id == orderItemId);
        if (orderItem!=null)
        {
            Items.Remove(orderItem);
        }

       
    }

    public void ChangeCountItem(long itemId,int newCount)
    {
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

    public void CheckOut(OrderAddress orderAddress)
    {
        this.Address = orderAddress;

    }
}