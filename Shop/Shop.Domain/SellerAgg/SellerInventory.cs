using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.SellerAgg;

public class SellerInventory : BaseEntity //موجودی های فروشنده
{
    public long SellerId { get; internal set; }

    public long ProductId { get; private set; }

    public int Count { get; private set; }

    public int Price { get; private set; }

    public SellerInventory(long productId, int count, int price)
    {
        if (price < 1 || count < 0)
        {
            throw new InvalidDomainDataException("مقدار قیمت یا تعداد از یک  یا صفر کمتر میباشد");
        }
        ProductId = productId;
        Count = count;
        Price = price;
    }


}