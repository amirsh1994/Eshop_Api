using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories;

public class CategoryDomainService:ICategoryDomainService
{
    private readonly ICategoryRepository _repository;

    public CategoryDomainService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public bool IsExistsSlug(string slug)
    {
        throw new NotImplementedException();
    }
}