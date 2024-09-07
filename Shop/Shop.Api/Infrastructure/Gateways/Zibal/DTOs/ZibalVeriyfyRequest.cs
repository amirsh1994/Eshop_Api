namespace Shop.Api.Infrastructure.Gateways.Zibal.DTOs;

public class ZibalVeriyfyRequest
{
    public string Merchant { get; private set; }

    public long TrackId { get; private set; }

    public ZibalVeriyfyRequest(string merchant, long trackId)
    {
        Merchant = merchant;
        TrackId = trackId;
    }
}

public class ZibalVeriyfyResponse
{
    public DateTime PaidAt { get; set; }

    public int Amount { get; set; }

    public int Result { get; set; }

    public int Status { get; set; }

    public int? RefNumber { get; set; }

    public string Description { get; set; }

    public string CardNumber { get; set; }

    public string OrderId { get; set; }

    public string Message { get; set; }
}