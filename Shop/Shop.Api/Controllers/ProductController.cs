using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Api.ViewModels.Products;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.CrudeProduct)]
public class ProductController : ApiController
{
    private readonly IProductFacade _productFacade;

    public ProductController(IProductFacade productFacade)
    {
        _productFacade = productFacade;
    }
    [AllowAnonymous]
    [HttpGet]

    public async Task<ApiResult<ProductFilterResult?>> GetProductByFilter([FromQuery] ProductFilterParams filterParams)//باید از کوری دریافت بشه  بصورت پیشفرض میره از بادی دریافتش میکنه اگه همینطوری یه ابجکت بهش پاس بدیم
    {
        var result = await _productFacade.GetProductsByFilter(filterParams);//
        return QueryResult<ProductFilterResult?>(result);
    }


    [AllowAnonymous]
    [HttpGet("Shop")]
    public async Task<ApiResult<ProductShopResult>> GetProductForShopFilter([FromQuery] ProductShopFilterParam filterParams)
    {
        return QueryResult(await _productFacade.GetProductForShop(filterParams));
    }



    [HttpGet("{productId:long}")]
    public async Task<ApiResult<ProductDto?>> GetProductById(long productId)
    {
        var result = await _productFacade.GetProductById(productId);
        return QueryResult<ProductDto>(result);
    }

    [AllowAnonymous]
    [HttpGet("bySlug/{slug}")]
    public async Task<ApiResult<ProductDto?>> GetProductBySlug(string slug)
    {
        var result = await _productFacade.GetProductBySlug(slug);
        return QueryResult<ProductDto>(result);
    }


    [HttpPost]
    public async Task<ApiResult<long>> CreateProduct([FromForm] CreateProductViewModel model)
    {
        var command = new CreateProductCommand(model.Title, model.ImageFile, model.Description,
            model.CategoryId, model.SubCategoryId, model.FirstSubCategoryId, model.Slug, model.SeoDataViewModel.ToSeoData(), model.GetSpecification());
        var result = await _productFacade.CreateProduct(command);
        return CommandResult<long>(result);
    }

    [HttpPost("images")]

    public async Task<ApiResult> AddProductImage([FromForm] AddProductImageViewModel model)
    {
        var command = new AddProductImageCommand(model.ImageFile, model.ProductId, model.Sequence);
        var result = await _productFacade.AddImage(command);

        return CommandResult(result);
    }

    [HttpDelete("images")]
    public async Task<ApiResult> RemoveImage(RemoveProductImageCommand command)
    {
        var result = await _productFacade.RemoveImage(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditProduct([FromForm] EditProductViewModel model)
    {
        var command = new EditProductCommand(model.Id, model.Title, model.ImageFile, model.Description,
            model.CategoryId, model.SubCategoryId, model.FirstSubCategoryId, model.Slug, model.SeoDataViewModel.ToSeoData(), model.GetSpecification());
        var result = await _productFacade.EditProduct(command);
        return CommandResult(result);
    }
}

