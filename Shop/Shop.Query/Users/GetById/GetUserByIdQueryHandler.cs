using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetById;

public class GetUserByIdQueryHandler : IBaseQueryHandler<GetUserByIdQuery, UserDto?>
{
    private readonly ShopContext _context;

    public GetUserByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user=await _context.Users.FirstOrDefaultAsync(x=>x.Id==request.UserId,cancellationToken);

        if (user == null)
            return null;

        return await user.Map().SetUserRoleTitle(_context);
    }
}