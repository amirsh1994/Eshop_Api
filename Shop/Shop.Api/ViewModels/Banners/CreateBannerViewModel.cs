using Common.Application.Validation.CustomValidation.IFormFile;
using Shop.Domain.SiteEntities;
using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Banners;

public class CreateBannerViewModel
{
   
    public string Link { get; set; }

   
    public IFormFile ImageFile { get; set; }

    public BannerPositions Positions { get; set; }
}


public class EditBannerViewModel
{
    public long BannerId { get; set; }


    public string Link { get; set; }


  
    public IFormFile? ImageFile { get; set; }

    public BannerPositions Positions { get; set; }
}