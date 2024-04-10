using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetList;

public class GetBannerListQueryHandler : IBaseQueryHandler<GetBannerListQuery, List<BannerDto>>
{
    private readonly ShopContext _context;

    public GetBannerListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<BannerDto>> Handle(GetBannerListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Banners.OrderByDescending(x => x.Id).Select(x => new BannerDto()
        {
            Id = x.Id,
            CreationDate = x.CreationDate,
            ImageName = x.ImageName,
            Positions = x.Positions,
            Link = x.Link,
        }).ToListAsync(cancellationToken);
    }
}