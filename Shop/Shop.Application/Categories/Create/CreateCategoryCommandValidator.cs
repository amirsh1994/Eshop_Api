using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.Create;

public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.required("عنوان"));
        RuleFor(x => x.Slug)
            .NotEmpty().NotNull().WithMessage(ValidationMessages.required("Slug"));
    }
    
}