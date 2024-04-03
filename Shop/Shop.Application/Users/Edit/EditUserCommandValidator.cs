using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Users.Edit;

public class EditUserCommandValidator:AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .ValidPhoneNumber();

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("ایمیل نامعتبر است");

        RuleFor(x => x.Password)
            .MinimumLength(4).WithMessage("کلمه عبور باید بیشتر از 4 کاراکتر باشد");

        RuleFor(x => x.Avatar)
            .JustImageFile();
    }
}