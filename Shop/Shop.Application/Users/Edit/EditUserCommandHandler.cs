using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Edit;

internal class EditUserCommandHandler:IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _service;
    private readonly IFileService _fileService;

    public EditUserCommandHandler(IUserRepository repository, IUserDomainService service, IFileService fileService)
    {
        _repository = repository;
        _service = service;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user =await _repository.GetTracking(request.UserId);

        if (user == null)
            return OperationResult.NotFound();
        var oldAvatar = user.AvatarName;
        user.Edit(request.Name,request.Family,request.PhoneNumber,request.Email,request.Gender,_service);

        if (request.Avatar!=null)
        {
            var imageName = await _fileService.SaveFileAndGenerateName(request.Avatar, Directories.UserAvatar);
            user.SetAvatar(imageName);
        }


        DeleteOldAvatar(request.Avatar, oldAvatar);

        await _repository.Save();
        return OperationResult.Success();
    }

    private void DeleteOldAvatar(IFormFile?imageFile,string oldImage)
    {
        if(imageFile==null||oldImage=="avatar.png")
            return;
        _fileService.DeleteFile(Directories.UserAvatar,oldImage);
    }
}