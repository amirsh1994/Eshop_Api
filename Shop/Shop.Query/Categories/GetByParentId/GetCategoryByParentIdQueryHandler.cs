using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetByParentId;

internal class GetCategoryByParentIdQueryHandler:IBaseQueryHandler<GetCategoryByParentIdQuery,List<ChildCategoryDto>>
{
    private readonly ShopContext _context;

    public GetCategoryByParentIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<ChildCategoryDto>> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        var cats = await _context.Categories.Where(x => x.ParentId == request.ParentId).ToListAsync(cancellationToken);
        return cats.MapChildren();
    }
}