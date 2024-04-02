using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Banners.Edit;

public class EditBannerCommandHandler : IBaseCommandHandler<EditBannerCommand>
{
    private readonly IBannerRepository _repository;
    private readonly IFileService _fileService;

    public EditBannerCommandHandler(IBannerRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _repository.GetTracking(request.BannerId);

        if (banner == null)
            return OperationResult.NotFound();

        var bannerImage = banner.ImageName;
        var oldBannerImage = banner.ImageName;
        if (request.ImageFile != null)
            bannerImage = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.BannerImages);

        banner.Edit(request.Link, bannerImage, request.Positions);
        await _repository.Save();
        DeleteOldImageBanner(request.ImageFile,oldBannerImage);

        return OperationResult.Success();

        //
        //var newBannerImage =
        //oldBanner.Edit(request.Link,newBannerImage,request.Positions);
        //await _repository.Save();
        //_fileService.DeleteFile(Directories.BannerImages,oldBannerImage);
        //return OperationResult.Success();

    }

    private void DeleteOldImageBanner(IFormFile ? imageFile,string oldImageName)
    {
        if(imageFile!=null)
            _fileService.DeleteFile(Directories.BannerImages,oldImageName);
    }
}
