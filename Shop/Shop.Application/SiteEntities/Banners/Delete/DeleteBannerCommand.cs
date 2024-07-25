using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Banners.Delete;

public record DeleteBannerCommand(long BannerId) : IBaseCommand;


public class DeleteBannerCommandCommandHandler : IBaseCommandHandler<DeleteBannerCommand>
{
    private readonly IBannerRepository _repository;
    private readonly IFileService _fileService;

    public DeleteBannerCommandCommandHandler(IBannerRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {

        var banner = await _repository.GetTracking(request.BannerId);
        if (banner == null) return OperationResult.NotFound();
        _repository.Delete(banner);
        await _repository.Save();
        _fileService.DeleteFile(Directories.BannerImages, banner.ImageName);
        return OperationResult.Success();

    }
}



