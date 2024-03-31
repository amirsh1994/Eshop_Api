using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Domain.ProductAgg;

public class Product : AggregateRoot
{
    public string Title { get; private set; }

    public string ImageName { get; private set; }

    public string Description { get; private set; }

    public long CategoryId { get; private set; }

    public long SubCategoryId { get; private set; }

    public long FirstSubCategoryId { get; private set; }//Level 3 

    public string Slug { get; private set; }

    public SeoData SeoData { get; private set; }

    public List<ProductImage> Images { get; private set; }

    public List<ProductSpecification> Specifications { get; private set; }

    private Product()
    {

    }
    public Product(string title, string imageName, string description, long categoryId, long subCategoryId, long firstSubCategoryId,
        string slug, SeoData seoData, IProductDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        Guard(title, description, slug, domainService);
        Title = title;
        ImageName = imageName;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        FirstSubCategoryId = firstSubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
        Images = new List<ProductImage>();
        Specifications = new List<ProductSpecification>();
    }
    public void EditProduct(string title, string description, long categoryId, long subCategoryId, long firstSubCategoryId,
        string slug, SeoData seoData, IProductDomainService domainService)
    {
        Guard(title, description, slug, domainService);
        Title = title;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        FirstSubCategoryId = firstSubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }

    public void SetProductImage(string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        this.ImageName=imageName;
    }

    public void AddImage(ProductImage productImage)
    {
        productImage.ProductId = Id;
        Images.Add(productImage);
    }

    public void RemoveImage(long imageId)
    {
        var imageName = Images.FirstOrDefault(x => x.Id == imageId);
        if (imageName == null)
        {
            return;
        }
        Images.Remove(imageName);
    }

    public void SetSpecification(List<ProductSpecification> specifications)
    {
        specifications.ForEach(x => x.ProductId = Id);
        this.Specifications = specifications;
    }

    public void Guard(string title, string description, string slug, IProductDomainService domainService)
    {

        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
        if (slug != Slug)
        {
            if (domainService.IsSlugExists(slug.ToSlug()))
            {
                throw new SlugIsDuplicatedException("Slug is Duplicated.....");
            }
        }
    }




}