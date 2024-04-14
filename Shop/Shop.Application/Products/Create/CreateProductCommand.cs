using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Products.Create;

public class CreateProductCommand : IBaseCommand<long>

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
    // Add a parameterless constructor for EF Core and DI and fill all properties with default values
    //public CreateProductCommand()
    //{
    //    Title = string.Empty;
    //    ImageFile = null;
    //    Description = string.Empty;
    //    CategoryId = 0;
    //    SubCategoryId = 0;
    //    FirstSubCategoryId = 0;
    //    Slug = string.Empty;
    //    SeoData = new SeoData("aaaaa", "bbbbb", "ccccc", "ddddd", "ggggggggg", true);
    //    Specifications = new Dictionary<string, string>();
    //}

   
}