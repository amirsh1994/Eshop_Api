using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Products.Edit;

public class EditProductCommandValidator:AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage(ValidationMessages.required("slug"));
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));

        RuleFor(x => x.ImageFile)
            .JustImageFile();
    }
}