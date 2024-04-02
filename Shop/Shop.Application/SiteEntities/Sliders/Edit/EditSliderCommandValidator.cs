using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Sliders.Edit;

public class EditSliderCommandValidator : AbstractValidator<EditSliderCommand>
{
    public EditSliderCommandValidator()
    {
        RuleFor(x => x.ImageFile)
            .JustImageFile();

        RuleFor(x => x.Link)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("لینک"));

        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
    }
}