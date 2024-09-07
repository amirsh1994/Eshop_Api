using Common.Domain;

namespace Shop.Domain.OrderAgg;

public class OrderAddress:BaseEntity
{
    public long OrderId { get;internal set; }

    public string Shire { get; private set; }

    public string City { get; private set; }

    public string PostalCode { get; private set; }

    public string PostalAddress { get; private set; }

    public string PhoneNumber { get; private set; }

    public string Family { get; private set; }

    public string Name { get; private set; }

    public string NationalCode { get; private set; }

    //public Order Order { get; set; }

   

    public OrderAddress( string shire, string city, string postalCode, string postalAddress, string phoneNumber, string family, string nationalCode, string name)
    {
       
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Family = family;
        NationalCode = nationalCode;
        Name = name;
    }
}