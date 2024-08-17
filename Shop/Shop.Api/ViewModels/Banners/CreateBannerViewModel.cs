using Common.Application.Validation.CustomValidation.IFormFile;
using Shop.Domain.SiteEntities;
using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Banners;

public class CreateBannerViewModel
{
    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Link { get; set; }

    [Display(Name = "عکس بنر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس بنر نا معتبر می باشد")]
    public IFormFile ImageFile { get; set; }

    public BannerPositions Positions { get; set; }
}


public class EditBannerViewModel
{
    public long BannerId { get; set; }


    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Link { get; set; }


    [Display(Name = "عکس بنر")]
    [FileImage(ErrorMessage = "عکس بنر نا معتبر می باشد")]
    public IFormFile? ImageFile { get; set; }

    public BannerPositions Positions { get; set; }
}