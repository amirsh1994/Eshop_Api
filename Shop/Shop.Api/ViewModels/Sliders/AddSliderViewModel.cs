using Common.Application.Validation.CustomValidation.IFormFile;
using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Sliders;

public class AddSliderViewModel
{

    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url)]
    public string Link { get; set; }


    [Display(Name = "عکس اسلایدر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس نامعتبر می باشد")]
    public IFormFile ImageFileName { get; set; }


    [Display(Name = "عنوان ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }
}

public class EditSliderViewModel
{
    public long Id { get; set; }

    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url)]
    public string Link { get; set; }


    [Display(Name = "عکس اسلایدر")]
    [FileImage(ErrorMessage = "عکس نامعتبر می باشد")]
    public IFormFile? ImageFileName { get; set; }


    [Display(Name = "عنوان ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }
}