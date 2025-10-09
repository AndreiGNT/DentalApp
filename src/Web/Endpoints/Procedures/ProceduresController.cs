using DentalApp.Application.Common.Models;
using DentalApp.Application.Procedures.Commands.CreateProcedure;
using DentalApp.Application.Procedures.Commands.DeleteProcedure;
using DentalApp.Application.Procedures.Commands.UpdateProcedure;
using DentalApp.Application.Procedures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DentalApp.Web.Endpoints.Procedures;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Admin")]
public class ProceduresController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProceduresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/procedures
    [HttpGet]
    public async Task<ActionResult<List<ProcedureDto>>> GetAllProcedures()
    {
        var query = new GetProcedureQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET: api/procedures/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProcedureDto>> GetProcedureById(int id)
    {
        var query = new GetProcedureQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // POST: api/doctors
    [HttpPost]
    public async Task<ActionResult<int>> CreateProcedure([FromBody] CreateProcedureCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid procedure data.");
        }

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetProcedureById), new { id }, id);
    }

    // PUT: api/procedure/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProcedure(int id, [FromBody] UpdateProcedureCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Procedure ID mismatch.");
        }

        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/doctors/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProcedure(int id)
    {
        var command = new DeleteProcedureCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
