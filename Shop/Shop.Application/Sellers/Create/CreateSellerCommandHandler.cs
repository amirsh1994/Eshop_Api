using Common.Application;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Application.Sellers.Create;

public class CreateSellerCommandHandler : IBaseCommandHandler<CreateSellerCommand>
{

    private readonly ISellerRepository _repository;
    private readonly ISellerDomainService _service;

    public CreateSellerCommandHandler(ISellerRepository repository, ISellerDomainService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<OperationResult> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = new Seller(request.UserId, request.ShopName, request.NationalCode,_service);
        _repository.Add(seller);
        await _repository.Save();
        return OperationResult.Success();
    }
}