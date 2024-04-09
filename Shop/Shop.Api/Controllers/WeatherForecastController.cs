using System.Reflection.Metadata.Ecma335;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Query.Orders.GetById;

namespace Shop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
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


    }
}
