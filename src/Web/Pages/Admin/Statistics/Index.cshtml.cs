using DentalApp.Application.Common.Models;
using DentalApp.Application.Statistics.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DentalApp.Web.Pages.Admin.Statistics;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public List<AppointmentStatisticsDto> Statistics { get; set; } = new();

    public async Task OnGetAsync()
    {
        var result = await _mediator.Send(new GetAppointmentStatisticsQuery());
        Statistics = result ?? new();
    }
}
