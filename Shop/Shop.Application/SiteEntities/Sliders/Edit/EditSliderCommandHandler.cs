using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Edit;

internal class EditSliderCommandHandler : IBaseCommandHandler<EditSliderCommand>
{
    private readonly ISliderRepository _repository;
    private readonly IFileService _fileService;

    public EditSliderCommandHandler(ISliderRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _repository.GetTracking(request.SliderId);

        if (slider == null)
            return OperationResult.NotFound();

        var sliderImageName = slider.ImageName;
        var oldImageName = slider.ImageName;


        if (request.ImageFile != null)
            sliderImageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);

        slider.EditSlider(request.Title,request.Link,sliderImageName);
        await _repository.Save();
        DeleteOldImage(request.ImageFile, oldImageName);
        return OperationResult.Success();
    }

    private void DeleteOldImage(IFormFile? imageFile, string oldImageName)
    {
        if (imageFile!=null)
            _fileService.DeleteFile(Directories.SliderImages,oldImageName);
        
            
        
    }
}