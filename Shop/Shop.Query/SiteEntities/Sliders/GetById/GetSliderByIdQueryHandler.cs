using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetById;

public class GetSliderByIdQueryHandler:IBaseQueryHandler<GetSliderByIdQuery, SliderDto>
{
    private readonly ShopContext _context;

    public GetSliderByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SliderDto> Handle(GetSliderByIdQuery request, CancellationToken cancellationToken)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == request.SliderId, cancellationToken);
        if (slider == null)
            return null;
        return new SliderDto()
        {
            Id = slider.Id,
            ImageName = slider.ImageName,
            Link = slider.Link,
            CreationDate = slider.CreationDate,
            Title = slider.Title

        };
    }
}