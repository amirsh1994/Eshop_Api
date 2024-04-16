using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Presentation.Facade.SiteEntiries.Banner;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers;

[Authorize]
public class BannerController : ApiController
{

    private readonly IBannerFacade _bannerFacade;

    public BannerController(IBannerFacade bannerFacade)
    {
        _bannerFacade = bannerFacade;
    }

    [HttpGet]
    public async Task<ApiResult<List<BannerDto>>> GetBanners()
    {
        var result = await _bannerFacade.GetBanners();
        return QueryResult(result);
    }


    [HttpGet("{id}")]
    public async Task<ApiResult<BannerDto?>> GetBannerById(long id)
    {
        var result = await _bannerFacade.GetBannerById(id);
        return QueryResult(result);
    }


    [HttpPost]
    public async Task<ApiResult> CreateBanner(CreateBannerCommand command)
    {
        var result = await _bannerFacade.CreateBanner(command);
        return CommandResult(result);
    }


    [HttpPut]
    public async Task<ApiResult> EditBanner(EditBannerCommand command)
    {
        var result = await _bannerFacade.EditBanner(command);
        return CommandResult(result);

    }
}

