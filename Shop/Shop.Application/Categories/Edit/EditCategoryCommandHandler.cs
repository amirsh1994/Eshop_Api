using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.Edit;

public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _domainService;

    public EditCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService domainService)
    {
        this._categoryRepository = categoryRepository;
        this._domainService = domainService;
    }

    public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var oldCate =await _categoryRepository.GetTracking(request.Id);
        if (oldCate is null)
        {
            return OperationResult.NotFound();
        }
        oldCate.EditCategory(request.Title,request.Slug,request.SeoData,_domainService);
        await _categoryRepository.Save();
        return OperationResult.Success();

    }
}