using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Products.RemoveImage;

internal class RemoveProductImageCommandHandler : IBaseCommandHandler<RemoveProductImageCommand>
{
    private readonly IProductRepository _repository;
    private readonly IFileService _fileService;

    public RemoveProductImageCommandHandler(IProductRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(RemoveProductImageCommand request, CancellationToken cancellationToken)
    {
        var p = await _repository.GetTracking(request.ProductId);
        if (p == null)
        {
            return OperationResult.NotFound();
        }
        var imageName = p.RemoveImage(request.ImageId);
        _fileService.DeleteFile(Directories.ProductGalleyImages,imageName);


        await _repository.Save();
        return OperationResult.Success();
    }
}