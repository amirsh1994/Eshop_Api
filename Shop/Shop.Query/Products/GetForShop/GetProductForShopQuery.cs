using Common.Query;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.SellerAgg.Enums;
using Shop.Infrastructure;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Categories;
using Shop.Query.Categories.DTOs;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetForShop;

public class GetProductForShopQuery : QueryFilter<ProductShopResult, ProductShopFilterParam>
{
    public GetProductForShopQuery(ProductShopFilterParam filterParam) : base(filterParam)
    {
    }
}

internal class GetProductForShopQueryHandler : IBaseQueryHandler<GetProductForShopQuery, ProductShopResult>
{
    private readonly DapperContext _dapperContext;
    private readonly ShopContext _context;

    public GetProductForShopQueryHandler(DapperContext dapperContext, ShopContext context)
    {
        _dapperContext = dapperContext;
        _context = context;
    }

    public async Task<ProductShopResult> Handle(GetProductForShopQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParam;
        var conditions = "";
        var orderBy = "";
        var inventoryOrderBy = "i.Price Asc";
        CategoryDto? selectedCategory = null;


        if (!string.IsNullOrWhiteSpace(@params.CategorySlug))
        {
            var category = await _context.Categories.FirstOrDefaultAsync(f => f.Slug == @params.CategorySlug, cancellationToken);

            if (category != null)
            {
                conditions += @$" and (A.CategoryId={category.Id} or A.SubCategoryId={category.Id}
                              or A.SecondarySubCategoryId={category.Id})";
                selectedCategory = category.Map();
            }
        }

        if (!string.IsNullOrWhiteSpace(@params.Search))
        {
            conditions += $" and A.Title Like N'%{@params.Search}%'";
        }

        if (@params.OnlyAvailableProducts)
        {
            conditions += " and A.Count>=1";
        }

        if (@params.JustHasDiscount)
        {
            conditions += " and A.DiscountPercentage>0";
            inventoryOrderBy = "i.DiscountPercentage Desc";
        }

        orderBy = @params.SearchOrderBy switch
        {
            ProductSearchOrderBy.Cheapest => "A.Price Asc",
            ProductSearchOrderBy.Expensive => "A.Price Desc",
            ProductSearchOrderBy.Latest => "A.Id Desc",
            _ => "p.Id"
        };

        using var connection=_dapperContext.CreateConnection();
        var skip = (@params.PageId - 1) * @params.Take;

        var sql = @$"SELECT Count(A.Title)
            FROM (Select p.Title , i.Price  , i.Id as InventoryId , i.DiscountPercentage , i.Count,
                        p.CategoryId,p.SubCategoryId,p.SecondarySubCategoryId, p.Id as Id , s.Status
                            ,ROW_NUMBER() OVER(PARTITION BY p.Id ORDER BY {inventoryOrderBy} ) AS RN
            From {_dapperContext.Products} p
            left join {_dapperContext.Inventories} i on p.Id=i.ProductId
            left join {_dapperContext.Sellers} s on i.SellerId=s.Id)A
            WHERE  A.RN = 1 and A.Status=@status{conditions}";

        var resultSql = @$"SELECT A.Slug,A.Id ,A.Title,A.Price,A.InventoryId,A.DiscountPercentage,A.ImageName
            FROM (Select p.Title , i.Price  , i.Id as InventoryId , i.DiscountPercentage,p.ImageName , i.Count,
                        p.CategoryId,p.SubCategoryId,p.SecondarySubCategoryId, p.Slug , p.Id as Id , s.Status
                            ,ROW_NUMBER() OVER(PARTITION BY p.Id ORDER BY {inventoryOrderBy}) AS RN
            From {_dapperContext.Products} p
            left join {_dapperContext.Inventories} i on p.Id=i.ProductId
            left join {_dapperContext.Sellers} s on i.SellerId=s.Id)A
            WHERE  A.RN = 1 and A.Status=@status  {conditions} order By {orderBy} offset @skip ROWS FETCH NEXT @take ROWS ONLY";



        var count = await connection.QueryFirstAsync<int>(sql, new { status = SellerStatus.Accepted });
        var result = await connection.QueryAsync<ProductShopDto>(resultSql,
            new { skip, take = @params.Take, status = SellerStatus.Accepted });
        var model = new ProductShopResult()
        {
            
            FilterParam = @params,
            Data = result.ToList(),
            CategoryDto = selectedCategory
        };
        model.GeneratePaging(@params.Take, @params.PageId, count);
        return model;

    }
}