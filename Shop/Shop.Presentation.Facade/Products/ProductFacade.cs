﻿using Common.Application;
using Common.CacheHelper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Products.GetForShop;

namespace Shop.Presentation.Facade.Products;

public class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _distributedCache;
    public ProductFacade(IMediator mediator, IDistributedCache distributedCache)
    {
        _mediator = mediator;
        _distributedCache = distributedCache;
    }

    public async Task<OperationResult<long>> CreateProduct(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditProduct(EditProductCommand command)
    {
        await _distributedCache.RemoveAsync(CacheKeys.Product(command.Slug));
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddImage(AddProductImageCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Status == OperationResultStatus.Success)
        {
            var product = await GetProductById(command.ProductId);
            await _distributedCache.RemoveAsync(CacheKeys.Product(product.Slug));
        }
        return result;
    }

    public async Task<OperationResult> RemoveImage(RemoveProductImageCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Status == OperationResultStatus.Success)
        {
            var product = await GetProductById(command.ProductId);
            await _distributedCache.RemoveAsync(CacheKeys.Product(product.Slug));
        }

        return result;
    }

    public async Task<ProductDto?> GetProductById(long productId)
    {
        return await _mediator.Send(new GetProductByIdQuery(productId));
    }

    public async Task<ProductDto?> GetProductBySlug(string slug)
    {
        return await _distributedCache.GetOrSet(CacheKeys.Product(slug), () =>
        {
            return _mediator.Send(new GetProductBySlugQuery(slug));
        });
    }
    public async Task<ProductFilterResult?> GetProductsByFilter(ProductFilterParams filterParams)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams));
    }

    public async Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParam)
    {
        return await _mediator.Send(new GetProductForShopQuery(filterParam));
    }
}