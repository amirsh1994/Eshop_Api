using System.Reflection.Metadata.Ecma335;
using Common.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.Create;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Orders.GetById;

namespace Shop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICategoryFacade _categoryFacade;

        public WeatherForecastController(IMediator mediator, ICategoryFacade categoryFacade)
        {
            _mediator = mediator;
            this._categoryFacade = categoryFacade;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(5));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        //[HttpGet("{id:long}")]
        //public async Task<IActionResult> GetCategoryById(int id)
        //{
        //    var model = await _categoryFacade.GetCategoryById(id);
        //    return Ok(model);
        //}


        [Route("/test")]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _categoryFacade.Create(command);

            if (result.Status == OperationResultStatus.Success)

                return Ok(result.Message);

            return BadRequest(result.Message);  //BadRequest(result.Message)  این دو دستور برای اطلاعات اشتباه ار�

        }
    }
}

