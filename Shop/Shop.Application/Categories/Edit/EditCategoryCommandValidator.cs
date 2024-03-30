using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.Edit;

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.required("عنوان"));
        RuleFor(x => x.Slug)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.required("Slug"));
    }
}