using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.SiteEntiries.Banner;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.CrudBanner)]
public class BannerController : ApiController
{

    private readonly IBannerFacade _bannerFacade;

    public BannerController(IBannerFacade bannerFacade)
    {
        _bannerFacade = bannerFacade;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<ApiResult<List<BannerDto>>> GetBanners()
    {
        var result = await _bannerFacade.GetBanners();
        return QueryResult(result);
    }


    [HttpGet("{bannerId}")]
    public async Task<ApiResult<BannerDto?>> GetBannerById(long bannerId)
    {
        var result = await _bannerFacade.GetBannerById(bannerId);
        return QueryResult(result);
    }


    [HttpPost]
    public async Task<ApiResult> CreateBanner([FromForm]CreateBannerCommand command)
    {
        var result = await _bannerFacade.CreateBanner(command);
        return CommandResult(result);
    }


    [HttpPut]
    public async Task<ApiResult> EditBanner([FromForm]EditBannerCommand command)
    {
        var result = await _bannerFacade.EditBanner(command);
        return CommandResult(result);

    }

    [HttpDelete("{bannerId}")]
    public async Task<ApiResult> Delete(long bannerId)
    {
        var result = await _bannerFacade.DeleteBanner(bannerId);
        return CommandResult(result);
    }
}

