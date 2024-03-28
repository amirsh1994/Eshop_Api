using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.SellerAgg.Enums;

namespace Shop.Domain.SellerAgg;

public class Seller : AggregateRoot
{
    public long UserId { get; private set; }

    public string ShopName { get; private set; }

    public string NationalCode { get; private set; }

    public SellerStatus Status { get; private set; }

    public List<SellerInventory> Inventories { get; private set; }

    public DateTime? LastUpdate { get; private set; }

    public Seller(long userId, string shopName, string nationalCode)
    {
        Guard(shopName,nationalCode);
        UserId = userId;
        ShopName = shopName;
        NationalCode = nationalCode;
        Inventories = new List<SellerInventory>();
    }

    private Seller()
    {
            
    }

    public void ChangeStatus(SellerStatus status)
    {
        this.Status = status;
        LastUpdate = DateTime.Now;
    }

    public void Edit(string shopName, string nationalCode)
    {
        Guard(shopName, nationalCode);
        this.NationalCode = nationalCode;
        this.ShopName = shopName;
    }

    public void AddInventory(SellerInventory inventory)
    {
        if (Inventories.Any(x=>x.ProductId==inventory.ProductId))
        {
            throw new InvalidDomainDataException("مقدار محصول با ایدی یکسان نمیتواند یه موجودی  فروشنده اضافه شود");
        }
        Inventories.Add(inventory);
    }

    public void EditInventory(SellerInventory newInventory)
    {
        var oldInventory = Inventories.FirstOrDefault(x => x.Id == newInventory.Id);
        if (oldInventory==null)
        {
            return;
        }
        Inventories.Remove(oldInventory);
        Inventories.Add(newInventory);
    }

    public void DeleteInventory(long inventoryId)
    {
        var currentInventory = Inventories.FirstOrDefault(x => x.Id == inventoryId);
        if (currentInventory!=null)
        {
            Inventories.Remove(currentInventory);
        }
        else
        {
            throw new NullOrEmptyDomainDataException("محصول یافت نشد.....");
        }
    }

    public void Guard(string shopName, string nationalCode)
    {
        NullOrEmptyDomainDataException.CheckString(shopName, nameof(shopName));
        NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));
        if (IranianNationalIdChecker.IsValid(nationalCode) == false)
        {
            throw new InvalidDomainDataException("کد ملی نامعتبر می باشد");

        }
    }
}