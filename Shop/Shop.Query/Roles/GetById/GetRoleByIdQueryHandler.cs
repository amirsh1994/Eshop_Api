using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetById;

public class GetRoleByIdQueryHandler:IBaseQueryHandler<GetRoleByIdQuery,RoleDto?>
{
    private readonly ShopContext _context;

    public GetRoleByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role=await _context.Roles.FirstOrDefaultAsync(x=>x.Id==request.RoleId,cancellationToken);
        if (role==null)
        {
            return null;
        }

        return new RoleDto()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Title = role.Title,
            Permissions = role.Permissions.Select(x=>x.Permission).ToList(),
        };
    }
}