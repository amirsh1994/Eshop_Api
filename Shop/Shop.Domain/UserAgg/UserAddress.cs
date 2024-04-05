using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.UserAgg;

public class UserAddress : BaseEntity
{
    public long UserId { get; internal set; }

    public string Shire { get; private set; }

    public string City { get; private set; }

    public string PostalCode { get; private set; }

    public string PostalAddress { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public string Name { get; private set; }

    public string Family { get; private set; }

    public string NationalCode { get; private set; }

    public bool ActiveAddress { get; private set; }

    public UserAddress(string shire, string city, string postalCode, string postalAddress, PhoneNumber phoneNumber, string family, string nationalCode, string name)
    {
        Guard(shire, name, city, postalCode, postalAddress, phoneNumber, family, nationalCode);
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Family = family;
        NationalCode = nationalCode;
        Name = name;
        ActiveAddress = false;
    }

    public void Edit(string shire,string name, string city, string postalCode, string postalAddress, PhoneNumber phoneNumber, string family, string nationalCode)
    {
        Guard(shire,name ,city, postalCode, postalAddress, phoneNumber, family, nationalCode);
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Family = family;
        NationalCode = nationalCode;
        Name=name;

    }

    public void Guard(string shire,string name ,string city, string postalCode, string postalAddress, PhoneNumber phoneNumber, string family, string nationalCode)
    {
        if (phoneNumber == null)
            throw new NullOrEmptyDomainDataException("شماره تلفن نمیتواند نال باشد در دامین چک شده است");
        NullOrEmptyDomainDataException.CheckString(shire, nameof(shire));
        NullOrEmptyDomainDataException.CheckString(city, nameof(city));
        NullOrEmptyDomainDataException.CheckString(postalCode, nameof(postalCode));
        NullOrEmptyDomainDataException.CheckString(postalAddress, nameof(postalAddress));
        NullOrEmptyDomainDataException.CheckString(name, nameof(name));
        NullOrEmptyDomainDataException.CheckString(family, nameof(family));
        NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));
        if (IranianNationalIdChecker.IsValid(nationalCode) == false)
        {
            throw new InvalidDomainDataException("کد ملی نادرست می باشد");
        }
    }

    public void SetActive()
    {
        ActiveAddress = true;
    }
}