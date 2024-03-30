using FluentValidation;

namespace Shop.Application.Orders.AddItem;

public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
{
    public AddOrderCommandValidator()
    {
        RuleFor(x => x.Count).GreaterThanOrEqualTo(1).WithMessage("تعداد بیشتر از 1 یا مساوی باشد ");
    }
}