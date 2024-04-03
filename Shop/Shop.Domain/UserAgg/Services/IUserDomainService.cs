namespace Shop.Domain.UserAgg.Services;

public interface IUserDomainService
{
    bool IsEmailExists(string email);

    bool IsPhoneNumberExists(string phoneNumber);
}