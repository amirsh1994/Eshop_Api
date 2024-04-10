using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByFilter;

public class GetUserByFilterQueryHAndler:IBaseQueryHandler<GetUserByFilterQuery, UserFilterResult>
{
    private readonly ShopContext _context;

    public GetUserByFilterQueryHAndler(ShopContext context)
    {
        _context = context;
    }

    public async Task<UserFilterResult> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params=request.FilterParam;

        var result =  _context.Users.OrderByDescending(x => x.Id).AsQueryable();


        if(!string.IsNullOrWhiteSpace(@params.Email))
            result=result.Where(x => x.Email.Contains(@params.Email));

        if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
            result = result.Where(x => x.PhoneNumber.Contains(@params.PhoneNumber));

        if (@params.Id!=null)
            result = result.Where(x => x.Id==@params.Id);

        var skip = (@params.PageId - 1) * @params.Take;

        var model = new UserFilterResult()
        {
            Data =await result
                .Skip(skip)
                .Take(@params.Take)
                .Select(x=>x.MapFilterData())
                .ToListAsync(cancellationToken),
              
            FilterParam = @params
        };


        model.GeneratePaging(result,@params.Take,@params.PageId);

        return model;
    }
}