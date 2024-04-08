using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetList;

internal class GetCategoryListQueryHandler : IBaseQueryHandler<GetCategoryListQuery, List<CategoryDto>>
{
    private readonly ShopContext _context;

    public GetCategoryListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Categories
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
        return model.Map();
        //var result = model.Select(x => x.Map()).ToList();
        //return result;
    }
}