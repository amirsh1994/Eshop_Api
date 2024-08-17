using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Users;

public class ChangePasswordViewModel
{
    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]

    public string CurrentPassword { get; set; }



    [Display(Name = "کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از5 کاراکتر باشد")]

    public string Password { get; set; }




    [Display(Name = "تکرار کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Compare(nameof(Password), ErrorMessage = "کلمه های عبور یکسان نمی باشد ")]

    public string ConfirmedPassword { get; set; }
}