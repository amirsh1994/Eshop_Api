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
        throw new NotImplementedException();
    }

    public bool NationalCodeExistsInDataBase(string nationalCode)
    {
        throw new NotImplementedException();
    }
}