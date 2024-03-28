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

    public string PhoneNumber { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public Gender Gender { get; private set; }

    public List<UserRole> UserRoles { get; private set; } = new();

    public List<UserAddress> UserAddresses { get; private set; } = new();

    public List<Wallet> Wallets { get; private set; } = new();


    public User(string name, string family, string phoneNumber, string email, string password, Gender gender, IDomainUserService domainUserService)
    {
        Guard(phoneNumber,email,domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
    }


    public void Edit(string name, string family, string phoneNumber, string email, Gender gender, IDomainUserService domainUserService)
    {
        Guard(phoneNumber,email,domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public void AddAddress(UserAddress userAddress)
    {
        userAddress.UserId = Id;
        this.UserAddresses.Add(userAddress);
    }

    public void EditAddress(UserAddress newUserAddress)
    {
        var oldAddress = UserAddresses.FirstOrDefault(x => x.Id == newUserAddress.Id);
        if (oldAddress == null)
        {
            throw new NullOrEmptyDomainDataException("Address Not Found...");
        }
        UserAddresses.Remove(oldAddress);
        UserAddresses.Add(newUserAddress);
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
        userRoles.ForEach(x=>x.UserId=Id);
        UserRoles.Clear();
        UserRoles.AddRange(userRoles);
    }

    public static User RegisterUser(string email,string password, string phoneNumber, IDomainUserService domainUserService)
    {
        return new User("","", phoneNumber, email,password,Gender.None,domainUserService);
    }
    public void Guard(string phoneNumber, string email,IDomainUserService domainUserService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        NullOrEmptyDomainDataException.CheckString(email, nameof(email));
        if (phoneNumber.Length != 11)
        {
            throw new InvalidDomainDataException("شماره موبایل 11 رقم می باشد حتما نه کمتر ونه بیشتر ");
        }

        if (email.IsValidEmail()==false)
        {
            throw new InvalidDomainDataException("ایمیل نامعتبر می باشد ");
        }

        if (phoneNumber!=PhoneNumber)
        {
            if (domainUserService.IsPhoneNumberExists(phoneNumber))
            {
                throw new InvalidDomainDataException("شماره موبایل تکراری هستش  ");
            }
        }
        if (email != Email)
        {
            if (domainUserService.IsPhoneNumberExists(email))
            {
                throw new InvalidDomainDataException("  ایمیل تکراری هست و قبلا تو دیتابیس وجود داشته ");
            }
        }
    }

}