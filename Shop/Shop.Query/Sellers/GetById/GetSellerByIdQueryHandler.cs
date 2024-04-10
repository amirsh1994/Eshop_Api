using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetById;

public class GetSellerByIdQueryHandler:IBaseQueryHandler<GetSellerByIdQuery, SellerDto?>
{
    private readonly ShopContext _context;

    public GetSellerByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SellerDto?> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
    {
        var seller =await _context.Sellers.FirstOrDefaultAsync(x=>x.Id==request.Id, cancellationToken);


        return seller.Map();
    }
}