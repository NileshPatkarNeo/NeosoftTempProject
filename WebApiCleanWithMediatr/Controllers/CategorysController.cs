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
    public class CategorysController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CategorysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategorysCommand command)
        {
            var category = await _mediator.Send(command);
            Log.Information("Category Info: {@category}", category);
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var query = new GetCategoryByIdQuery(id);
            var category = await _mediator.Send(query);
            
            if (category == null)
                return NotFound();
            Log.Information("Category Info: {@category}", category);
            return Ok(category);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<CategoryDto>> Update(int id, UpdateCategoryCommand command)
        //{
        //    if (id != command.CategoryId)
        //        return BadRequest();

        //    var category = await _mediator.Send(command);

        //    if (category == null)
        //        return NotFound();

        //    Log.Information("Category Info: {@category}", category);
        //    return Ok(category);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var command = new DeleteCategoryCommand(id);
        //    var result = await _mediator.Send(command);

        //    if (!result)
        //        return NotFound();

        //    return NoContent();
        //}

    }
}
