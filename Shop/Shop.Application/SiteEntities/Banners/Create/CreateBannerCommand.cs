using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;

namespace Shop.Application.SiteEntities.Banners.Create;

public class CreateBannerCommand : IBaseCommand
{
    public string Link { get; private set; }

    public IFormFile ImageFile { get; private set; }

    public BannerPositions Positions { get; private set; }

    public CreateBannerCommand(string link, IFormFile imageFile, BannerPositions positions)
    {
        Link = link;
        ImageFile = imageFile;
        Positions = positions;
    }
}