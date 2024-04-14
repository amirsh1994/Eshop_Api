using AngleSharp.Io;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products.Create;

internal class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand,long>
{
    private readonly IProductRepository _repository;
    private readonly IProductDomainService _service;
    private readonly IFileService _fileService;

    public CreateProductCommandHandler(IProductRepository repository, IProductDomainService service, IFileService fileService)
    {
        _repository = repository;
        _service = service;
        _fileService = fileService;
    }
   

    public async Task<OperationResult<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
        var pro = new Product(request.Title, imageName, request.Description, request.CategoryId, request.SubCategoryId
        , request.FirstSubCategoryId, request.Slug, request.SeoData, _service);
         _repository.Add(pro);
        List<ProductSpecification> productSpecification = new List<ProductSpecification>();
        request.Specifications.ToList().ForEach(p =>
        {
            productSpecification.Add(new ProductSpecification(p.Key, p.Value));
        });
        pro.SetSpecification(productSpecification);
        await _repository.Save();
        return OperationResult<long>.Success(pro.Id);

       
    }
}