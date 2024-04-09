using Common.Query;
using Shop.Infrastructure;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetByFilter;

public class GetProductByFilterQueryHandler : IBaseQueryHandler<GetProductByFilterQuery, ProductFilterResult>
{
    private readonly ShopContext _context;

    public GetProductByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParam;

        var result = _context.Products.OrderByDescending(x => x.Id).AsQueryable();
        if (string.IsNullOrWhiteSpace(@params.Slug) == false)
            result = result.Where(x => x.Slug == @params.Slug);

        if (string.IsNullOrWhiteSpace(@params.Title) == false)
            result = result.Where(x => x.Title.Contains(@params.Title));


        if (@params.Id != null)
            result = result.Where(x => x.Id == @params.Id);

        var skip = (@params.PageId - 1) * @params.Take;
        var model = new ProductFilterResult()
        {
            Data =  result.Skip(skip).Take(@params.Take).Select(x => x.MapListData()).ToList(),
            FilterParam = @params
        };
        model.GeneratePaging(result,@params.Take,@params.PageId);
        return model;

    }
}