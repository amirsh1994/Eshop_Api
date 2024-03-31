using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Products.Create;

public class CreateProductCommand : IBaseCommand
{
    public string Title { get; private set; }

    public IFormFile ImageFile { get; private set; }

    public string Description { get; private set; }

    public long CategoryId { get; private set; }

    public long SubCategoryId { get; private set; }

    public long FirstSubCategoryId { get; private set; }//Level 3 

    public string Slug { get; private set; }

    public SeoData SeoData { get; private set; }

    public Dictionary<string, string> Specifications { get; private set; }

    public CreateProductCommand(string title, IFormFile imageFile, string description, long categoryId,
        long subCategoryId, long firstSubCategoryId, string slug, SeoData seoData, Dictionary<string, string> specifications)
    {
        Title = title;
        ImageFile = imageFile;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        FirstSubCategoryId = firstSubCategoryId;
        Slug = slug;
        SeoData = seoData;
        Specifications = specifications;
    }

}