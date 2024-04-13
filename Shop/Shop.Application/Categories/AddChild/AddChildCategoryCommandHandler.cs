using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.AddChild;

public class AddChildCategoryCommandHandler : IBaseCommandHandler<AddChildCategoryCommand,long>
{
    private readonly ICategoryRepository _repository;
    private readonly ICategoryDomainService _service;

    public AddChildCategoryCommandHandler(ICategoryRepository repository, ICategoryDomainService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<OperationResult<long>> Handle(AddChildCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetTracking(request.ParentId);
        if (category is null)
        {
            return OperationResult<long>.NotFound();
        }
        category.AddChild(request.Title, request.Slug, request.SeoData, _service);
        await _repository.Save();
        //var child=category.Children.FirstOrDefault(x=>x.ParentId==request.ParentId);
        return OperationResult<long>.Success(category.Id);
    }
}