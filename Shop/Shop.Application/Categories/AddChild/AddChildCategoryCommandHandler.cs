using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.AddChild;

public class AddChildCategoryCommandHandler : IBaseCommandHandler<AddChildCategoryCommand>
{
    private readonly ICategoryRepository _repository;
    private readonly ICategoryDomainService _service;

    public AddChildCategoryCommandHandler(ICategoryRepository repository, ICategoryDomainService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<OperationResult> Handle(AddChildCategoryCommand request, CancellationToken cancellationToken)
    {
        var parentCategory = await _repository.GetTracking(request.ParentId);
        if (parentCategory is null)
        {
            return OperationResult.NotFound();
        }
        parentCategory.AddChild(request.Title, request.Slug, request.SeoData, _service);
        await _repository.Save();
        return OperationResult.Success();
    }
}