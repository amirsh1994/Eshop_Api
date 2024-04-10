using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application._Utilities;
using Shop.Application.Categories;
using Shop.Application.Products;
using Shop.Application.Roles.Create;
using Shop.Application.Sellers;
using Shop.Application.Users;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.SellerAgg.Services;
using Shop.Domain.UserAgg.Services;
using Shop.Infrastructure;
using Shop.Presentation.Facade;
using Shop.Query.Categories.GetById;

namespace Shop.Config;

public static class ShopBootstrapper
{

    public static void RegisterShopDependency(this IServiceCollection service,string connectionString)
    {
        InfrastructureBootstrapper.Init(service,connectionString);

        service.AddMediatR(typeof(Directories).Assembly);//Application Command Has Been Registered
        service.AddMediatR(typeof(GetCategoryByIdQuery).Assembly);//query Has Been Registered In MediatR
        service.AddTransient<IProductDomainService,ProductDomainService>();
        service.AddTransient<ICategoryDomainService,CategoryDomainService>();
        service.AddTransient<ISellerDomainService,SellerDomainService>();
        service.AddTransient<IUserDomainService,UserDomainService>();
        service.AddValidatorsFromAssembly(typeof(CreateRoleCommandValidator).Assembly);
        service.InitFacadeDependency();




    }
}