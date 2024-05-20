using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApiCleanWithMediatr.Application.Commands;
using WebApiCleanWithMediatr.Application.Queries;
using WebApiCleanWithMediatr.Domain.DTO;

namespace WebApiCleanWithMediatr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        //Testinggggg
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            Log.Information("Order Info: {@order}", order);
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var order = await _mediator.Send(query);

            if (order == null)
                return NotFound();
            Log.Information("Order Info: {@order}", order);
            return Ok(order);
        }

        // Update and Delete methods can be added similarly as needed
    }
}
