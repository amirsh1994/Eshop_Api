using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetList;

public class GetSliderListQuery:IBaseQuery<List<SliderDto>>
{
    
}


public class GetSliderListQueryHandler : IBaseQueryHandler<GetSliderListQuery, List<SliderDto>>
{
    private readonly ShopContext _context;

    public GetSliderListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<SliderDto>> Handle(GetSliderListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sliders.OrderByDescending(x => x.Id).Select(x => new SliderDto()
        {
            Id = x.Id,
            CreationDate = x.CreationDate,
            ImageName = x.ImageName,
            Title = x.Title,
            Link = x.Link,
        }).ToListAsync(cancellationToken);
    }
}