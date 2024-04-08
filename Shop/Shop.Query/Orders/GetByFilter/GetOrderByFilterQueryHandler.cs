using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetByFilter;

internal class GetOrderByFilterQueryHandler:IBaseQueryHandler<GetOrderByFilterQuery, OrderFilterResult>
{
    private readonly ShopContext _context;

    public GetOrderByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<OrderFilterResult> Handle(GetOrderByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParam;
        var result = _context.Orders.OrderByDescending(x => x.Id).AsQueryable();

        if (@params.Status != null)
            result = result.Where(x => x.Status == @params.Status);
        if (@params.UserId != null)
            result = result.Where(x => x.UserId == @params.UserId);
        if (@params.StartDate != null)
            result = result.Where(x => x.CreationDate >= @params.StartDate.Value.Date);
        if (@params.EndDate != null)
            result = result.Where(x => x.CreationDate <= @params.EndDate.Value.Date);



        var skip = (@params.PageId - 1) * @params.Take;
        var model = new OrderFilterResult()
        {
            Data = await result.Skip(skip).Take(@params.Take).Select(order => order.MapFilterData(_context)).ToListAsync(cancellationToken),
            FilterParam = @params

        };
        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}