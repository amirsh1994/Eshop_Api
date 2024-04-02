using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Banners.Edit;

public class EditBannerCommandValidator:AbstractValidator<EditBannerCommand>
{
    public EditBannerCommandValidator()
    {
        RuleFor(x => x.ImageFile)
            .JustImageFile();

        RuleFor(x => x.Link)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("لینک"));
    }
}
