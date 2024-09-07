using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.CheckOut;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;

namespace Shop.Api.Controllers;

[Authorize]
public class OrderController : ApiController
{

    private readonly IOrderFacade _orderFacade;

    public OrderController(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }

    [PermissionChecker(Permission.Order_Management)]
    [HttpGet]
    public async Task<ApiResult<OrderFilterResult?>> GetOrderByFilter([FromQuery] OrderFilterParams filterParams)
    {
        var result = await _orderFacade.GetOrdersByFilter(filterParams);
        return QueryResult(result);
    }

    [Authorize]
    [HttpGet("current/filter")]
    public async Task<ApiResult<OrderFilterResult?>> GetUserOrdersByFilter(int pageId=1,int take=10,OrderStatus status=OrderStatus.Finally)
    {
        var filterParams = new OrderFilterParams()
        {
            PageId = pageId,
            Status = status,
            UserId = User.GetUserId(),
            Take = take,
            EndDate = null,
            StartDate = null
        };
        var result = await _orderFacade.GetOrdersByFilter(filterParams);
        return QueryResult(result);
    }



    [HttpGet("{orderId}")]
    public async Task<ApiResult<OrderDto?>> GetOrderById(long orderId)
    {
        var result = await _orderFacade.GetOrderById(orderId);
        return QueryResult(result);
    }



    [HttpGet("current")]
    public async Task<ApiResult<OrderDto?>> GetCurrentOrder()
    {
        var result = await _orderFacade.GetCurrentOrderByUserId(User.GetUserId());
        return QueryResult(result);
    }




    [HttpPost]
    public async Task<ApiResult> AddOrderItem(AddOrderItemCommand command)
    {
        var result = await _orderFacade.AddOrderItem(command);
        return CommandResult(result);
    }





    [HttpPost("Checkout")]
    public async Task<ApiResult> CheckOrder(CheckOutOrderCommand command)
    {
        var result = await _orderFacade.OrderCheckOut(command);
        return CommandResult(result);
    }




    [HttpPut("OrderItem/IncreaseCount")]
    public async Task<ApiResult> IncreaseOrderItemCount(IncreaseOrderItemCountCommand command)
    {
        var result = await _orderFacade.IncreaseItemCount(command);
        return CommandResult(result);
    }




    [HttpPut("OrderItem/DecreaseCount")]
    public async Task<ApiResult> DecreaseOrderItemCount(DecreaseOrderItemCountCommand command)
    {
        var result = await _orderFacade.DecreaseItemCount(command);
        return CommandResult(result);
    }





    [HttpDelete("OrderItem/{itemId:long}")]
    public async Task<ApiResult> RemoveOrderItem(long itemId)
    {
        var userId = User.GetUserId();
        var result = await _orderFacade.RemoveOrderItem(new RemoveOrderItemCommand(userId, itemId));
        return CommandResult(result);
    }

}

