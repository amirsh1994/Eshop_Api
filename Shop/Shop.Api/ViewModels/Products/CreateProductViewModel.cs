using Common.Application.Validation.CustomValidation.IFormFile;
using System.ComponentModel.DataAnnotations;
using Common.Domain.ValueObjects;
using Newtonsoft.Json;

namespace Shop.Api.ViewModels.Products;

public class CreateProductViewModel
{
    public string Title { get; set; }

    public IFormFile ImageFile { get; set; }

    public string Description { get; set; }

    public long CategoryId { get; set; }

    public long SubCategoryId { get; set; }

    public long FirstSubCategoryId { get; set; }//Level 3 

    public string Slug { get; set; }

    public SeoDataViewModel SeoDataViewModel { get; set; }

    public string Specification { get; set; }

    public Dictionary<string, string> GetSpecification()

        => JsonConvert.DeserializeObject<Dictionary<string, string>>(Specification);


}

public class EditProductViewModel
{
    public long Id { get; set; }

    public string Title { get; set; }

    public IFormFile?  ImageFile { get; set; }

    public string Description { get; set; }

    public long CategoryId { get; set; }

    public long SubCategoryId { get; set; }

    public long FirstSubCategoryId { get; set; }//Level 3 

    public string Slug { get; set; }

    public SeoDataViewModel SeoDataViewModel { get; set; }

    public string Specification { get; set; }

    public Dictionary<string, string> GetSpecification()

        => JsonConvert.DeserializeObject<Dictionary<string, string>>(Specification);


}

public class SeoDataViewModel
{
    public string MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? MetaKeyWords { get; set; }

    public bool IndexPage { get; set; }

    public string? Canonical { get; set; }

    public string? Schema { get; set; }

    public SeoData ToSeoData()
    {
        return new SeoData(MetaKeyWords, MetaDescription, MetaTitle, Canonical, Schema, IndexPage);
    }
}