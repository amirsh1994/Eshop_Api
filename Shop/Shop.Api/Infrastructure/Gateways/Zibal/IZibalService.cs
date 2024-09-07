using System.Net.Mime;
using System.Text;
using Common.Application.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shop.Api.Infrastructure.Gateways.Zibal.DTOs;

namespace Shop.Api.Infrastructure.Gateways.Zibal;

public interface IZibalService
{
    Task<string> StartPay(ZibalPaymentRequest request);//درخواست رو ارسال کرد کالبک نتیجه میده بعدش ما هدایتش میکنیم تو این ادرس

    Task<ZibalVeriyfyResponse?> Verify(ZibalVeriyfyRequest request);//مرجنت و ترک ایدی رو بهش میدیم میره اعتبار سنجی میکنه اگه معتبر بود این نتیجه رو برمیگردونه
}

public class ZibalService : IZibalService
{
    #region Injection

    private readonly HttpClient _client;
    private static JsonSerializerSettings _defaultSerializedSetting = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    public ZibalService(HttpClient client)
    {
        _client = client;
    }

    #endregion


    public async Task<string> StartPay(ZibalPaymentRequest request)
    {
        var body = JsonConvert.SerializeObject(request, _defaultSerializedSetting);
        var content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json);
        var httpResponseMessage = await _client.PostAsync(ZibalOptions.RequestUrl, content);
        if (!httpResponseMessage.IsSuccessStatusCode)
            throw new InvalidCommandException(httpResponseMessage.StatusCode.ToString());
        var paymentResult = await httpResponseMessage.Content.ReadFromJsonAsync<ZibalPaymentResult>();
        if (paymentResult!.Result==100)
        {
            return $"{ZibalOptions.PaymentUrl}{paymentResult.TrackId}";
        }
        throw new InvalidCommandException(ZibalTranslator.TranslateResult(paymentResult.Result));
    }

    public async Task<ZibalVeriyfyResponse?> Verify(ZibalVeriyfyRequest request)
    {
        var body = JsonConvert.SerializeObject(request, _defaultSerializedSetting);
        var content = new StringContent(body,Encoding.UTF8,MediaTypeNames.Application.Json);
        var httpResponseMessage = await _client.PostAsync(ZibalOptions.VerifyUrl, content);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var readFromJsonAsync = await httpResponseMessage.Content.ReadFromJsonAsync<ZibalVeriyfyResponse>();
            return readFromJsonAsync;
        }
        throw new InvalidCommandException(httpResponseMessage.StatusCode.ToString());
    }
}