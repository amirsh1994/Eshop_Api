using Common.Domain;

namespace Shop.Domain.OrderAgg.ValueObjects;

public class OrderDiscount : ValueObject
{
    public string DiscountTitle { get; set; }

    public int DiscountAmount { get; set; }

    public OrderDiscount(string discountTitle, int discountAmount)
    {
        DiscountTitle = discountTitle;
        DiscountAmount = discountAmount;
    }

    private OrderDiscount()
    {

    }
}