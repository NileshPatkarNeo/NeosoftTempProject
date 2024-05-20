using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApiCleanWithMediatr.Application.Commands;
using WebApiCleanWithMediatr.Application.Queries;
using WebApiCleanWithMediatr.Domain.DTO;
using WebApiCleanWithMediatr.Domain.Entities;

namespace WebApiCleanWithMediatr.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(CreateProductCommand command)
        {
            var product = await _mediator.Send(command);
            Log.Information("Products Info:{@product}", product);
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);

            if (product == null)
                return NotFound();
            Log.Information("Products Info:{@product}", product);
            return Ok(product);
        }

        //[HttpPut]
        //public 
    }
}
