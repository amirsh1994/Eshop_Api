using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetList;

public class GetRoleListQueryHandler:IBaseQueryHandler<GetRoleListQuery,List<RoleDto>>
{
    private readonly ShopContext _context;

    public GetRoleListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        var result =await _context.Roles.Select(x => new RoleDto()
        {
            Id = x.Id,
            CreationDate = x.CreationDate,
            Title = x.Title,
            Permissions = x.Permissions.Select(p=>p.Permission).ToList()
        }).ToListAsync(cancellationToken);

        return result;
    }
}