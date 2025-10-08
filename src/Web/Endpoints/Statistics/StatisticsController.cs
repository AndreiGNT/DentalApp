using DentalApp.Application.Common.Models;
using DentalApp.Application.Statistics.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalApp.Web.Endpoints.Statistics;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<AppointmentStatisticsDto>>> GetStatistics()
    {
        var result = await _mediator.Send(new GetAppointmentStatisticsQuery());
        return Ok(result);
    }
}
