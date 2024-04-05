using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;

namespace Shop.Domain.UserAgg;

public class User : AggregateRoot
{
    private User()
    {

    }
    public string Name { get; private set; }

    public string Family { get; private set; }

    public string AvatarName { get; private set; }

    public string PhoneNumber { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public Gender Gender { get; private set; }

    public List<UserRole> UserRoles { get; private set; } = new();

    public List<UserAddress> UserAddresses { get; private set; } = new();

    public List<Wallet> Wallets { get; private set; } = new();


    public User(string name, string family, string phoneNumber, string email, string password, Gender gender, IUserDomainService userDomainService)
    {
        Guard(phoneNumber, email, userDomainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
        AvatarName = "avatar.png";
    }


    public void Edit(string name, string family, string phoneNumber, string email, Gender gender, IUserDomainService userDomainService)
    {
        Guard(phoneNumber, email, userDomainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public void SetAvatar(string newImageName)
    {
        if (string.IsNullOrWhiteSpace(newImageName))
            newImageName = "avatar.png";

        this.AvatarName = newImageName;
    }

    public void AddAddress(UserAddress userAddress)
    {
        userAddress.UserId = Id;
        this.UserAddresses.Add(userAddress);
    }

    public void EditAddress(UserAddress newUserAddress, long addressId)
    {
        var oldAddress = UserAddresses.FirstOrDefault(x => x.Id == addressId);
        if (oldAddress == null)
        {
            throw new NullOrEmptyDomainDataException("Address Not Found...");
        }
        oldAddress.Edit(newUserAddress.Shire, newUserAddress.Name, newUserAddress.City,
            newUserAddress.PostalCode, newUserAddress.PostalAddress, newUserAddress.PhoneNumber, newUserAddress.Family, newUserAddress.NationalCode);
    }

    public void DeleteAddress(long userAddressId)
    {
        var userAddress = UserAddresses.FirstOrDefault(x => x.Id == userAddressId);
        if (userAddress == null)
        {
            throw new NullOrEmptyDomainDataException("Address Not Found...");
        }

        UserAddresses.Remove(userAddress);
    }

    public void ChargeWallet(Wallet wallet)
    {
        wallet.UserId = Id;
        Wallets.Add(wallet);
    }

    public void SetRoles(List<UserRole> userRoles)
    {
        userRoles.ForEach(x => x.UserId = Id);
        UserRoles.Clear();
        UserRoles.AddRange(userRoles);
    }

    public static User RegisterUser( string password, string phoneNumber, IUserDomainService userDomainService)
    {
        return new User("", "", phoneNumber, null, password, Gender.None, userDomainService);
    }
    public void Guard(string phoneNumber, string email, IUserDomainService userDomainService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        NullOrEmptyDomainDataException.CheckString(email, nameof(email));
        if (phoneNumber.Length != 11)
        {
            throw new InvalidDomainDataException("شماره موبایل 11 رقم می باشد حتما نه کمتر ونه بیشتر ");
        }

        if (email.IsValidEmail() == false)
        {
            throw new InvalidDomainDataException("ایمیل نامعتبر می باشد ");
        }

        if (phoneNumber != PhoneNumber)
        {
            if (userDomainService.IsPhoneNumberExists(phoneNumber))
            {
                throw new InvalidDomainDataException("شماره موبایل تکراری هستش  ");
            }
        }
        if (email != Email)
        {
            if (userDomainService.IsPhoneNumberExists(email))
            {
                throw new InvalidDomainDataException("  ایمیل تکراری هست و قبلا تو دیتابیس وجود داشته ");
            }
        }
    }

}