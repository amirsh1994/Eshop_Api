using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.SiteEntiries.Slider;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.CrudeSlider)]
public class SliderController : ApiController
{
    private readonly ISliderFacade _sliderFacade;

    public SliderController(ISliderFacade sliderFacade)
    {
        _sliderFacade = sliderFacade;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<List<SliderDto>>> GetSliders()
    {
        var result = await _sliderFacade.GetSliders();
        return QueryResult(result);
    }




    [HttpGet("{id}")]
    public async Task<ApiResult<SliderDto?>> GetSlider(long id)
    {
        var result = await _sliderFacade.GetSliderById(id);
        return QueryResult(result);
    }


    [HttpPost]
    public async Task<ApiResult> CreateSlider(CreateSliderCommand command)
    {
        var result = await _sliderFacade.CreateSlider(command);
        return CommandResult(result);

    }



    [HttpPut]
    public async Task<ApiResult> EditSlider(EditSliderCommand command)
    {
        var result = await _sliderFacade.EditSlider(command);
        return CommandResult(result);
    }

}

