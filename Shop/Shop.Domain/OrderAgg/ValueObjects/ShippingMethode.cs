using Common.Domain;

namespace Shop.Domain.OrderAgg.ValueObjects;

public class ShippingMethode:ValueObject
{
    //private ShippingMethode()
    //{
            
    //}
    public string ShippingType { get;private set; }

    public int ShippingCost { get; private set; }

    public ShippingMethode(string shippingType, int shippingCost)
    {
        ShippingType = shippingType;
        ShippingCost = shippingCost;
    }

   
}