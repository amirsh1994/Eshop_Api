using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Orders.CheckOut;

public class CheckOutOrderCommandValidator:AbstractValidator<CheckOutOrderCommand>
{
    public CheckOutOrderCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("نام"));
        RuleFor(x => x.Family)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("فامیلی"));
        RuleFor(x => x.City)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("شهر"));
        RuleFor(x => x.Shire)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("استان"));
        RuleFor(x => x.PostalAddress)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("ادرس پستی"));
        RuleFor(x => x.PostalCode)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("کد پستی"));
        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("شماره موبایل"))
            .MaximumLength(11).WithMessage("شماره موبایل نباید بیشتر از 11 رقم باشد")
            .MinimumLength(11).WithMessage("شماره موبایل نباید کمتر از 11 رقم باشد");

        RuleFor(x => x.NationalCode)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("شماره ملی"))
            .MaximumLength(11).WithMessage("شماره ملی نامعتبر هست")
            .MinimumLength(11).WithMessage("شماره ملی نامعتبر هست")
            .ValidNationalId();



    }
}