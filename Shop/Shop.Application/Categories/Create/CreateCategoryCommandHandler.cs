using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.Create;

public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _domainService;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService domainService)
    {
        _categoryRepository = categoryRepository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var cat = new Category(request.Title, request.Slug, request.SeoData, _domainService);
        _categoryRepository.Add(cat);
        await _categoryRepository.Save();
        return OperationResult.Success();

    }
}