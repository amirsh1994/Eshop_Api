﻿using Common.Application;

namespace Shop.Application.Sellers.EditInventory;

public class EditSellerInventoryCommand : IBaseCommand
{
    public long SellerId { get; private set; }

    public long InventoryId { get; private set; }

    public int Count { get; private set; }

    public int Price { get; private set; }

    public int? DiscountPercentage { get; private set; }

    public EditSellerInventoryCommand(long sellerId,  int count, int price, int? Discountpercentage, long inventoryId)
    {
        SellerId = sellerId;
        Count = count;
        Price = price;
        DiscountPercentage = Discountpercentage;
        InventoryId = inventoryId;
    }
}