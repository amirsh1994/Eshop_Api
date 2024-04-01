using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Products.AddImage;

public class AddProductImageCommandHandler : IBaseCommandHandler<AddProductImageCommand>//برا موقعی که میخواییم تو گالری ثبت شه
{
    private readonly IProductRepository _repository;
    private readonly IFileService _fileService;

    public AddProductImageCommandHandler(IProductRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var p = await _repository.GetTracking(request.ProductId);
        if (p == null)
        {
            return OperationResult.NotFound();
        }

        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleyImages);
        var productImages = new ProductImage(imageName, request.Sequence);
        p.AddImage(productImages);
       




        await _repository.Save();
        return OperationResult.Success();
    }
}