using Common.Application;
using FluentValidation;
using FluentValidation.Validators;

namespace Shop.Application.Orders.RemoveItem;

public record RemoveOrderItemCommand(long UserId, long ItemId) : IBaseCommand;