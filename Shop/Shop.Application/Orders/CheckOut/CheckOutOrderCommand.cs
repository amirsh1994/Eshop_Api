using Common.Application;

namespace Shop.Application.Orders.CheckOut;

public class CheckOutOrderCommand:IBaseCommand
{
    public long UserId { get;private  set; }//برای این ایدی یوزر رو میگیریم که فقط بتونه اردر پندینگ خودشو دستکاری کنه نه هکه اردر هارو بتونه دستکاری کنه

    public string Shire { get;private  set; }

    public string City { get;  private set; }

    public string PostalCode { get;  private set; }

    public string PostalAddress { get;  private set; }

    public string PhoneNumber { get;  private set; }

    public string Family { get;  private set; }

    public string Name { get; private set; }

    public string NationalCode { get;  private set; }

    public long ShippingMethodeId { get;private set; }

    public CheckOutOrderCommand(long userId, string shire, string city, string postalCode, string postalAddress, string phoneNumber, string family, string nationalCode, string name, long shippingMethodeId)
    {
        UserId = userId;
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Family = family;
        NationalCode = nationalCode;
        Name = name;
        ShippingMethodeId = shippingMethodeId;
    }
}