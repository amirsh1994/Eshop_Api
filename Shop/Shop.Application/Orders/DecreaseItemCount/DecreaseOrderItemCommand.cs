using Common.Application;
using FluentValidation;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.DecreaseItemCount;

public record DecreaseOrderItemCommand(long UserId, long ItemId, int Count) : IBaseCommand;

    



public class DecreaseOrderItemCommandHandler:IBaseCommandHandler<DecreaseOrderItemCommand>
{
    private readonly IOrderRepository _repository;

    public DecreaseOrderItemCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(DecreaseOrderItemCommand request, CancellationToken cancellationToken)
    {
        var currentOrder =await _repository.GetCurrentUserOrder(request.UserId);
        if (currentOrder== null)
        {
            return OperationResult.NotFound();
        }
        currentOrder.DecreaseItemCount(request.ItemId,request.Count);
        await _repository.Save();
        return OperationResult.Success();
    }
}



public class DecreaseOrderItemCommandValidator:AbstractValidator<DecreaseOrderItemCommand>
{
    public DecreaseOrderItemCommandValidator()
    {
        RuleFor(x => x.Count).GreaterThanOrEqualTo(1).WithMessage("تعداد بیشتر از 1 یا مساوی باشد ");
    }
}