using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByFilter;

public class GetSellerByFilterQueryHandler:IBaseQueryHandler<GetSellerByFilterQuery, SellerFilterResult>
{
    private readonly ShopContext _context;

    public GetSellerByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SellerFilterResult> Handle(GetSellerByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params=request.FilterParam;
        var result =  _context.Sellers.OrderByDescending(x=>x.Id).AsQueryable();


        if (string.IsNullOrWhiteSpace(@params.NationalCode) == false)
            result = result.Where(x => x.NationalCode.Contains(@params.NationalCode));

        if (string.IsNullOrWhiteSpace(@params.ShopName) == false)
            result = result.Where(x => x.ShopName.Contains(@params.ShopName));

        var skip = (@params.PageId - 1) * @params.Take;

        var sellerResult = new SellerFilterResult()
        {
            FilterParam = @params,
            Data =await result.Skip(skip).Take(@params.Take).Select(seller=>seller.Map()).ToListAsync(cancellationToken)

        };


        sellerResult.GeneratePaging(result,@params.Take,@params.PageId);
        return sellerResult;

    }
}