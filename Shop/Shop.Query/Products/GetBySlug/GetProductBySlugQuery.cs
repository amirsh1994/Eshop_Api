using Common.Query;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetBySlug;

public class GetProductBySlugQuery:IBaseQuery<ProductDto?>
{
    public string Slug { get; private set; }
}