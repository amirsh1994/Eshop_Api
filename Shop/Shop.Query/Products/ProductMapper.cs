using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products;

public static class ProductMapper
{
    public static ProductDto? Map(this Product? p)
    {
        if (p==null)
        {
            return null;
        }
        return new ProductDto()
        {
            Id = p.Id,
            CreationDate = p.CreationDate,
            Description = p.Description,
            ImageName = p.ImageName,
            Slug = p.Slug,
            Title = p.Title,
            SeoData = p.SeoData,
            SpecificationsDtos = p.Specifications.Select(x => new ProductSpecificationDto()
            {
                Key = x.Key,
                Value = x.Value
            }).ToList(),
            ImagesDtos = p.Images.Select(x => new ProductImageDto()
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                ImageName = x.ImageName,
                ProductId = x.ProductId,
                Sequence = x.Sequence
            }).ToList(),
            Category = new ()
            {
                Id = p.CategoryId
            },
            SubCategory = new ()
            {
                Id = p.SubCategoryId
            },
            SecondarySubCategory =p.SecondarySubCategory != null? new ()
            {
                Id =(long) p.SecondarySubCategory
            }:null,
        };
    }
    public static ProductFilterData MapListData(this Product p)
    {
        return new ProductFilterData()
        {
            Id = p.Id,
            CreationDate = p.CreationDate,
            ImageName = p.ImageName,
            Slug = p.Slug,
            Title = p.Title,
            
        };
    }

    public static async Task  SetCategories(this ProductDto pDto,ShopContext context)
    {
        var category = await context.Categories
            .Where(x=>x.Id==pDto.Category.Id)
            .Select(x=>new ProductCategoryDto()
            {
                Id = x.Id,
                ParentId = x.ParentId,
                SeoData = x.SeoData,
                Slug = x.Slug,
                Title = x.Title,
            })
            .FirstOrDefaultAsync();
            

        var subcategory = await context.Categories
            .Where(x => x.Id == pDto.SubCategory.Id)
            .Select(x => new ProductCategoryDto()
            {
                Id = x.Id,
                ParentId = x.ParentId,
                SeoData = x.SeoData,
                Slug = x.Slug,
                Title = x.Title,
            })
            .FirstOrDefaultAsync();
        if (pDto.SecondarySubCategory!=null)
        {
            var secondarySubcategory = await context.Categories
                .Where(x => x.Id == pDto.SecondarySubCategory.Id)
                .Select(x => new ProductCategoryDto()
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    SeoData = x.SeoData,
                    Slug = x.Slug,
                    Title = x.Title,
                })
                .FirstOrDefaultAsync();

            if (secondarySubcategory != null)
            {
                pDto.SecondarySubCategory = secondarySubcategory;
            }
        }
       


        if (category!=null)
        {
            pDto.Category = category;
        }
        if (subcategory != null)
        {
            pDto.SubCategory = subcategory;
        }
       
       
    }
}