using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.SiteEntities;

public class Banner:BaseEntity
{
    public string Link { get; private set; }

    public string ImageName { get; private set; }

    public  BannerPositions Positions { get; private set; }

    private Banner()
    {
        
    }

    public Banner(string link, string imageName, BannerPositions positions)
    {
        Guard(link, imageName);
        Link = link;
        ImageName = imageName;
        Positions = positions;
    }

    public void Edit(string link, string imageName, BannerPositions positions)
    {
        Guard(link,imageName);
        Link = link;
        ImageName = imageName;
        Positions = positions;
    }

    public void Guard(string link, string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(link,nameof(link));
        NullOrEmptyDomainDataException.CheckString(imageName,nameof(imageName));
    }
}

public enum BannerPositions
{
    زیر_اسلایدر,
    سمت_چپ_اسلایدر,
    بالای_اسلایدر,
    سمت_راست_شگفت_انگیز,
    وسط_صفحه
}