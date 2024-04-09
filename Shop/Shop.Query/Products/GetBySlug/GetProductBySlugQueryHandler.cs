using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetBySlug;

public class GetProductBySlugQueryHandler:IBaseQueryHandler<GetProductBySlugQuery, ProductDto?>
{
    private readonly ShopContext _context;

    public GetProductBySlugQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Slug == request.Slug, cancellationToken);
        var model = product.Map();
        if (model == null)
        {
            return null;
        }
        await model.SetCategories(_context);
        return model;
    }
}