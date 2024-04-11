using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Application.Sellers;

public class SellerDomainService:ISellerDomainService
{
    private readonly ISellerRepository _repository;

    public SellerDomainService(ISellerRepository repository)
    {
        _repository = repository;
    }

    public bool CheckSellerInfo(Seller seller)
    {
        var sellerIsExists = _repository.Exists(x => x.NationalCode == seller.NationalCode || x.UserId == seller.UserId);

        return !sellerIsExists;  
        //یعنی اگر فروشنده با ایدی یوزر وکد ملی در دیتابیس وجود داشته باشد تابع ریپوزیتوری  مقدار ترو رو بر میگرداند ولی ما مقدار نقیض اون رو بر میگردونیم یعنی اینه فروشنده جدید نمیتواند به سیستم ما اضافه شود چون شناسه و کد ملی تکراری دارد 

    }

    public bool NationalCodeExistsInDataBase(string nationalCode)
    {
        return _repository.Exists(x =>x.NationalCode==nationalCode);
    }
}