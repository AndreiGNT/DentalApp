using DentalApp.Application.Doctors.Commands.CreateDoctor;
using DentalApp.Application.Doctors.Commands.UpdateDoctor;
using DentalApp.Application.Doctors.Queries;
using DentalApp.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using DentalApp.Application.Doctors.Commands.DeleteDoctor;

namespace DentalApp.Web.Endpoints.Doctors;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Admin")]
public class DoctorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/doctors
    [HttpGet]
    public async Task<ActionResult<List<DoctorDto>>> GetAllDoctors()
    {
        var query = new GetDoctorsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET: api/doctors/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
    {
        var query = new GetDoctorQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // POST: api/doctors
    [HttpPost]
    public async Task<ActionResult<int>> CreateDoctor([FromBody] CreateDoctorCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid doctor data.");
        }

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetDoctorById), new { id }, id);
    }

    // PUT: api/doctors/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Doctor ID mismatch.");
        }

        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/doctors/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteDoctor(int id)
    {
        var command = new DeleteDoctorCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
