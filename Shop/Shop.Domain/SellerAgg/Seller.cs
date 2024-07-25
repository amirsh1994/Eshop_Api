using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.SellerAgg.Enums;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Domain.SellerAgg;

public class Seller : AggregateRoot
{
    public long UserId { get; private set; }

    public string ShopName { get; private set; }

    public string NationalCode { get; private set; }

    public SellerStatus Status { get; private set; }

    public List<SellerInventory> Inventories { get; private set; }//مئجودی کل فروشنده می باشد 

    public DateTime? LastUpdate { get; private set; }



    public Seller(long userId, string shopName, string nationalCode, ISellerDomainService domainService)
    {
        Guard(shopName, nationalCode);
        UserId = userId;
        ShopName = shopName;
        NationalCode = nationalCode;
        Inventories = new List<SellerInventory>();
        if (domainService.CheckSellerInfo(this) == false)
            throw new InvalidDomainDataException("// اظلاعات نامعتبر هست ....");
        //
    }

    private Seller()
    {

    }

    public void ChangeStatus(SellerStatus status)
    {
        this.Status = status;
        LastUpdate = DateTime.Now;
    }

    public void Edit(string shopName, string nationalCode, ISellerDomainService domainService)
    {
        Guard(shopName, nationalCode);
        if (nationalCode != NationalCode)//یعنی اینکه کد ملی رو تغییر داده 
            if (domainService.NationalCodeExistsInDataBase(nationalCode))
                throw new InvalidDomainDataException("کد ملی متعلق به شخص دیگری هست....");
        this.NationalCode = nationalCode;
        this.ShopName = shopName;
    }

    public void AddInventory(SellerInventory inventory)
    {
        if (Inventories.Any(x => x.ProductId == inventory.ProductId))
        {
            throw new InvalidDomainDataException("در این موجودی قبلا محصولی با ایدی که وارد کردید ثبت شده است");
        }
        Inventories.Add(inventory);
        

    }

    public void EditInventory(long newInventoryId, int count, int price, int? discount)
    {
        var oldInventory = Inventories.FirstOrDefault(x => x.Id == newInventoryId);
        if (oldInventory == null)
        {
            throw new NullOrEmptyDomainDataException("همچین موجودی با ایدی یافت نشد ");
        }
        //Todo Check Inventories
        oldInventory.Edit(count, price, discount);
    }

    //public void DeleteInventory(long inventoryId)
    //{
    //    var currentInventory = Inventories.FirstOrDefault(x => x.Id == inventoryId);
    //    if (currentInventory!=null)
    //    {
    //        Inventories.Remove(currentInventory);
    //    }
    //    else
    //    {
    //        throw new NullOrEmptyDomainDataException(" موجدی یافت نشد .....");
    //    }
    //}

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