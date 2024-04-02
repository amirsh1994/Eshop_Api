using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Sliders.Create;

public class CreateSliderCommandValidator:AbstractValidator<CreateSliderCommand>
{
    public CreateSliderCommandValidator()
    {
        RuleFor(x => x.ImageFile)
            .NotNull().WithMessage(ValidationMessages.required("عکس"))
            .JustImageFile();

        RuleFor(x => x.Link)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("لینک"));

        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
    }
}