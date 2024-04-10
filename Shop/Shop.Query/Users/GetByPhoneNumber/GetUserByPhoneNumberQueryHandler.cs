using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByPhoneNumber;

public class GetUserByPhoneNumberQueryHandler:IBaseQueryHandler<GetUserByPhoneNumberQuery,UserDto?>
{
    private readonly ShopContext _context;

    public GetUserByPhoneNumberQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var user =await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber,cancellationToken);

        if (user == null)
        {
            return null; // throw new NotFoundException("User Not Found"); این است که به دلیل اینکه از این کلاس اس
        }

        return await user.Map().SetUserRoleTitle(_context);
    }
}