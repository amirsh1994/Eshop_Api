using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Gateways.Zibal;
using Shop.Api.Infrastructure.Gateways.Zibal.DTOs;
using Shop.Api.ViewModels.Transactions;
using Shop.Application.Orders.Finally;
using Shop.Presentation.Facade.Orders;

namespace Shop.Api.Controllers;

public class TransactionController : ApiController
{
    private readonly IZibalService _zibalService;
    private readonly IOrderFacade _orderFacade;

    public TransactionController(IZibalService zibalService, IOrderFacade orderFacade)
    {
        _zibalService = zibalService;
        _orderFacade = orderFacade;
    }

    //if we call this API this will call ziball API And Its Give Us Back A return Url And zibal send some information to this url
    //یه زیبال میکه این اردر با این لیدی ومبلغ اومده لطفا وریفایش کن
    [HttpPost]
    [Authorize]
    public async Task<ApiResult<string?>> CreateTransaction(CreateTransactionViewModel command)
    {
        var order = await _orderFacade.GetOrderById(command.OrderId);
        if (order == null || order.Address==null || order.Methode==null)
            return CommandResult(OperationResult<string?>.NotFound());

        var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

        var result = await _zibalService.StartPay(new ZibalPaymentRequest//همون ادرس بازگشتی که کاربر قرار هدایت شه سمتش
        {
            Merchant = "zibal",
            Amount = order!.TotalPrice,
            CallBackUrl = $"{url}/api/Transaction?orderId={order.Id}&errorRedirect={command.ErrorCallBackUrl}&successRedirect={command.SuccessCallBackUrl}",
            Description = $"پرداخت سفارش با شناسه {order.Id}",
            Mobile = User.GetPhoneNumber(),
            LinkToPay = false,
            SendSms = false
        });
        return CommandResult(OperationResult<string>.Success(result));
    }
    [HttpGet]//zibal will send this params to our APIs And We Verify this transaction if its successfully transaction we then finally our order.
    public async Task<IActionResult> Verify(long orderId, long trackId, int success, string errorRedirect, string successRedirect)//ما اول یه درخواست پرداخت میزنیم بهدرگاهمون اون همون اطلاعات تو لینکس که بهش دادیم به ما می دهد
    {
        if (success == 0)
            return Redirect(errorRedirect);

        var order = await _orderFacade.GetOrderById(orderId);
        if (order == null)
            return Redirect(errorRedirect);

        var result = await _zibalService.Verify(new ZibalVeriyfyRequest("zibal", trackId));
        if (result?.Result != 100)
            return Redirect(errorRedirect);

        if (result?.Amount != order.TotalPrice)
            return Redirect(errorRedirect);

        var operationResult = await _orderFacade.FinallyOrder(new OrderFinallyCommand(orderId));
        if (operationResult.Status == OperationResultStatus.Success)
            return Redirect(successRedirect);

        return Redirect(errorRedirect);
    }


}

