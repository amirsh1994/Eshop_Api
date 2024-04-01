using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Products.AddImage;

public class AddProductImageCommandValidator:AbstractValidator<AddProductImageCommand>
{
    public AddProductImageCommandValidator()
    {
        RuleFor(x => x.ImageFile)
            .JustImageFile()
            .NotNull().WithMessage(ValidationMessages.required("عکس"));

        RuleFor(x => x.Sequence)
            .GreaterThanOrEqualTo(0).WithMessage("باید از 0 بزرگتر یا مساوی باشه ");

    }
}