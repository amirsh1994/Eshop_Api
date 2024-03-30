using Common.Application;

namespace Shop.Application.Orders.AddItem;

public class AddOrderCommand : IBaseCommand
{
    public long InventoryId { get; private set; }

    public int Count { get; private set; }

    public long UserId { get; private set; }

    public AddOrderCommand(long inventoryId, int count, long userId)
    {
        InventoryId = inventoryId;
        Count = count;
        UserId = userId;
    }
}