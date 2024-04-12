using Shop.Domain.CategoryAgg;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories;

internal static class CategoryMapper
{
    public static CategoryDto? Map(this Category? category)
    {
        if (category == null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            Slug = category.Slug,
            SeoData = category.SeoData,
            Title = category.Title,
            Children = category.Children.MapChildren()

        };
    }

    public static List<CategoryDto> Map(this List<Category>categories)
    {
        var model=new List<CategoryDto>();
        categories.ForEach(x =>
        {
            model.Add(new CategoryDto()
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                Slug = x.Slug,
                SeoData = x.SeoData,
                Title = x.Title,
                Children = x.Children.MapChildren()
            });
        });
        return model;
    }

    public static List<ChildCategoryDto> MapChildren(this List<Category> children)
    {
        var model = new List<ChildCategoryDto>();
        children.ForEach(x =>
        {
            model.Add(new ChildCategoryDto()
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                Slug = x.Slug,
                SeoData = x.SeoData,
                Title = x.Title,
                ParentId = (long)x.ParentId,
                Children = x.Children.MapSecondaryChildren()
            });
        });
        return model;
    }

    private static List<SecondaryChildCategoryDto> MapSecondaryChildren(this List<Category> children)
    {
        var model = new List<SecondaryChildCategoryDto>();
        children.ForEach(x =>
        {
            model.Add(new SecondaryChildCategoryDto()
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                Slug = x.Slug,
                SeoData = x.SeoData,
                Title = x.Title,
                ParentId = (long)x.ParentId
            });
        });
        return model;
    }
}