using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Domain.CategoryAgg;

public class Category:AggregateRoot
{

    public string Title { get; private set; }

    public string Slug { get; private set; }

    public SeoData SeoData { get; private set; }

    public long? ParentId { get; private set; }

    public List<Category> Children { get; private set; }=new();

    private Category()
    {
            
    }

    public Category(string title, string slug, SeoData seoData, ICategoryDomainService domainService)
    {
        slug = slug?.ToSlug();
        Guard(title, slug, domainService);
        Title = title;
        Slug = slug;
        SeoData = seoData;
        Children = new List<Category>();
    }

    public void EditCategory(string title, string slug, SeoData seoData, ICategoryDomainService domainService)
    {
        slug = slug?.ToSlug();
        Guard(title,slug,domainService);
        Title = title;
        Slug = slug;
        SeoData = seoData;
    }

    public void AddChild(string title, string slug, SeoData seoData, ICategoryDomainService domainService)
    {
        Children.Add(new Category(title,slug,seoData,domainService)
        {
            ParentId = Id
        });
    }

    public void Guard(string title, string slug,ICategoryDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
        
        if (slug!=Slug)
            if (domainService.IsSlugExists(slug))
                throw new SlugIsDuplicatedException();
        
    }
}