using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users;

public class UserDomainService:IUserDomainService
{
    private readonly IUserRepository _repository;

    public UserDomainService(IUserRepository repository)
    {
        _repository = repository;
    }

    public bool IsEmailExists(string email)
    {
        throw new NotImplementedException();
    }

    public bool IsPhoneNumberExists(string phoneNumber)
    {
        throw new NotImplementedException();
    }
}