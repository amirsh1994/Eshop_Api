namespace Shop.Domain.CategoryAgg.Services;

public interface ICategoryDomainService
{
    bool IsExistsSlug(string slug);
}