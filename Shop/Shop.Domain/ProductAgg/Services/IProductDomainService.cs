namespace Shop.Domain.ProductAgg.Services;

public interface IProductDomainService
{
    bool IsSlugExists(string slug);
}