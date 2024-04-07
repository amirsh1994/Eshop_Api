using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetById;

internal class GetCategoryByIdQueryHandler : IBaseQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ShopContext _context;

    public GetCategoryByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x=>x.Id==request.CategoryId,cancellationToken);
        return category.Map();
    }
}