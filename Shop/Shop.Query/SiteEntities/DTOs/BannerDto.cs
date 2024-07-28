using Common.Query;
using Shop.Domain.SiteEntities;

namespace Shop.Query.SiteEntities.DTOs;

public class BannerDto:BaseDto
{
    public string Link { get;  set; }

    public string ImageName { get;  set; }

    public BannerPositions Positions { get; set; }
}




public class SliderDto:BaseDto
{
    public string Title { get; set; }

    public string Link { get; set; }

    public string ImageName { get; set; }

}

public class ShippingMethodDto : BaseDto
{
    public string Title { get; set; }
    public int Cost { get; set; }
}