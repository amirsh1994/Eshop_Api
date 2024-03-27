namespace Shop.Domain.UserAgg.Services;

public interface IDomainUserService
{
    bool IsEmailExists(string email);

    bool IsPhoneNumberExists(string phoneNumber);
}