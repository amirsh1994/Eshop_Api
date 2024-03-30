using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.AddChild;

public class AddChildCategoryCommandValidator:AbstractValidator<AddChildCategoryCommand>
{
    public AddChildCategoryCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.required("عنوان"));
        RuleFor(x => x.Slug)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.required("Slug"));
    }
}