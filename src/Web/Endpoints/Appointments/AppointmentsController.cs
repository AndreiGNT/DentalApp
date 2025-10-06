using DentalApp.Application.Appointments.Commands.CreateAppointment;
using DentalApp.Application.Appointments.Commands.DeleteAppointment;
using DentalApp.Application.Appointments.Commands.UpdateAppointment;
using DentalApp.Application.Appointments.Queries.GetAppointmentWithPagination;
using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;
using DentalApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentalApp.Web.Endpoints.Appointments;

[Route("api/[controller]")]
[ApiController]
[Application.Common.Security.Authorize]
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
    public async Task<ActionResult<List<AppointmentDto>>> GetAppointments([FromQuery] GetAppointmentsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET: api/appointments/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppointmentDto>> GetAppointmentById(int id)
    {
        var query = new GetAppointmentByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("mine")]
    public async Task<ActionResult<List<AppointmentDto>>> GetMine(
    [FromServices] UserManager<ApplicationUser> userManager,
    [FromQuery] GetAppointmentsQuery query)
    {
        var userId = userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        query = query with { UserId = userId };

        var result = await _mediator.Send(query);
        return Ok(result);
    }


    // POST: api/appointments
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateAppointmentCommand command)
    {
        // Preiau durata procedurii din Db
        var procedure = await _context.Procedures.FindAsync(command.ProcedureId);
        if (procedure == null)
            return BadRequest("Selected procedure not found.");

        // creez un nou comand cu EndTime calculat
        var commandWithEndTime = new CreateAppointmentCommand
        {
            UserId = command.UserId,
            DoctorId = command.DoctorId,
            ProcedureId = command.ProcedureId,
            StartTime = command.StartTime,
            EndTime = command.StartTime.Add(procedure.Duration)
        };

        var id = await _mediator.Send(commandWithEndTime);
        return CreatedAtAction(nameof(GetAppointmentById), new { id }, id);
    }

    // PUT: api/appointments/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateAppointmentCommand command)
    {
        if (id != command.Id) return BadRequest();
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

