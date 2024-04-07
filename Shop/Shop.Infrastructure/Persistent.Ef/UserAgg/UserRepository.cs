using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg;

public class UserRepository:BaseRepository<User>,IUserRepository
{
    public UserRepository(ShopContext context) : base(context)
    {
    }
}