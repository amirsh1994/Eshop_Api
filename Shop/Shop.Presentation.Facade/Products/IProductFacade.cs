using Common.Application;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Query.Products.DTOs;
using Shop.Query.Sellers.DTOs;

namespace Shop.Presentation.Facade.Products;

public interface IProductFacade
{
    Task<OperationResult<long>> CreateProduct(CreateProductCommand command);

    Task<OperationResult> EditProduct(EditProductCommand command);

    Task<OperationResult> AddImage(AddProductImageCommand command);

    Task<OperationResult> RemoveImage(RemoveProductImageCommand command);

    Task<ProductDto?> GetProductById(long productId);

    Task<SingleProductDto?> GetProductForSinglePage(string slug);

    Task<ProductDto?> GetProductBySlug(string slug);

    Task<ProductFilterResult> GetProductsByFilter(ProductFilterParams filterParams);

    Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParam);
}

public class SingleProductDto
{
    public ProductDto Product { get; set; }

    public List<InventoryDto> Inventories { get; set; }


}