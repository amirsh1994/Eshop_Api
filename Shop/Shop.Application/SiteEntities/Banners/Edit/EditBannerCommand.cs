using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;

namespace Shop.Application.SiteEntities.Banners.Edit;

public class EditBannerCommand : IBaseCommand
{
    public long BannerId { get; private set; }

    public string Link { get; private set; }

    public IFormFile? ImageFile { get; private set; }

    public BannerPositions Positions { get; private set; }

    public EditBannerCommand(string link, IFormFile? imageFile, BannerPositions positions, long bannerId)
    {
        Link = link;
        ImageFile = imageFile;
        Positions = positions;
        BannerId = bannerId;
    }
}
