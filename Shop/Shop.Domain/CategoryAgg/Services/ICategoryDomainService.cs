namespace Shop.Domain.CategoryAgg.Services;

public interface ICategoryDomainService
{
    bool IsSlugExists(string slug);
}