using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Users.ChargeWallet;

public class ChargeUserWalletCommandValidator:AbstractValidator<ChargeUserWalletCommand>
{
    public ChargeUserWalletCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(1000).WithMessage("شازژ کیف پول باید بیشتر از 1000 تومان باشد ");
    }
}