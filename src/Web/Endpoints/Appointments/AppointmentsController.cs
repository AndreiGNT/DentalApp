using DentalApp.Application.Appointments.Commands.CreateAppointment;
using DentalApp.Application.Appointments.Commands.DeleteAppointment;
using DentalApp.Application.Appointments.Commands.UpdateAppointment;
using DentalApp.Application.Appointments.Queries;
using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;
using DentalApp.Application.Common.Security;
using Microsoft.AspNetCore.Mvc;

namespace DentalApp.Web.Endpoints.Appointments;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _context;

    public AppointmentsController(IMediator mediator, IApplicationDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    // GET: api/appointments
    [HttpGet]
    public async Task<ActionResult<List<AppointmentDto>>> GetAppointments()
    {
        var query = new GetAppointmentsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET: api/appointments/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppointmentDto>> GetAppointmentById(int id)
    {
        var query = new GetAppointmentQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // POST: api/appointments
    [HttpPost]
    public async Task<ActionResult<int>> CreateAppointment([FromBody] CreateAppointmentCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid doctor data.");
        }

        // Preiau durata procedurii din Db
        var procedure = await _context.Procedures.FindAsync(command.ProcedureId);

        if (procedure == null)
        {
            return BadRequest("Selected procedure not found.");
        }

        // creez un nou comand cu EndTime calculat
        var commandWithEndTime = new CreateAppointmentCommand
        (
            command.UserId,
            command.DoctorId,
            command.ProcedureId,
            command.StartTime,
            command.StartTime.Add(procedure.Duration)
        );

        var id = await _mediator.Send(commandWithEndTime);

        return CreatedAtAction(nameof(GetAppointmentById), new { id }, id);
    }

    // PUT: api/appointments/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateAppointmentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Procedure ID mismatch.");
        }

        await _mediator.Send(command);

        return NoContent();
    }

    // DELETE: api/appointments/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteAppointmentCommand { Id = id };

        await _mediator.Send(command);
        return NoContent();
    }
}

