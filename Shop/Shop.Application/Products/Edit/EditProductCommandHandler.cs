using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products.Edit;

public class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly IProductDomainService _service;
    private readonly IFileService _fileService;

    public EditProductCommandHandler(IProductRepository repository, IProductDomainService service, IFileService fileService)
    {
        _repository = repository;
        _service = service;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var oldProduct = await _repository.GetTracking(request.Id);

        
        if (oldProduct == null)
        {
            return OperationResult.NotFound();
        }
        oldProduct.EditProduct(request.Title, request.Description, request.CategoryId, request.SubCategoryId, request.FirstSubCategoryId, request.Slug, request.SeoData, _service);

         string oldImage = oldProduct.ImageName;


        if (request.ImageFile != null)
        {
            var newImageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
            oldProduct.SetProductImage(newImageName);
        }



        var productSpecifications = new List<ProductSpecification>();
        request.Specifications.ToList().ForEach(x =>
        {
            productSpecifications.Add(new ProductSpecification(x.Key, x.Value));
        });
        oldProduct.SetSpecification(productSpecifications);

        await _repository.Save();

        RemoveOldImage(request.ImageFile,oldImage);

        return OperationResult.Success();
    }

    private void RemoveOldImage(IFormFile? imageFile, string oldImageName)
    {
        if (imageFile==null==false)
        {
            _fileService.DeleteFile(Directories.ProductImages,oldImageName);
        }
    }
}