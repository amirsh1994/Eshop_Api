using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.OrderAgg;

public class OrderItem : BaseEntity
{
    private OrderItem()
    {

    }

    public long OrderId { get; internal set; }

    public long InventoryId { get; private set; }

    public int Count { get; private set; }

    public int Price { get; set; }

    public int TotalPrice => Price * Count;

    public OrderItem(long orderId, long inventoryId, int count, int price)
    {
        CountGuard(count);
        PriceGuard(price);
        OrderId = orderId;
        InventoryId = inventoryId;
        Count = count;
        Price = price;
    }

    public void ChangeCount(int newCount)
    {
        CountGuard(newCount);
        this.Count=newCount;
    }

    public void SetPrice(int newPrice)
    {
        PriceGuard(newPrice);
        Price = newPrice;
    }

    public void PriceGuard(int newPrice)
    {
        if (newPrice<1)
        {
            throw new InvalidDomainDataException("مبلغ کالا از 1 کمتر است");
        }
    }
    public void CountGuard(int newCount)
    {
        if (newCount < 1)
        {
            throw new InvalidDomainDataException("تعداد کالا از 1 کمتر است");
        }
    }

}