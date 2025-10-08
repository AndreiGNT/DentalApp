using DentalApp.Application.Common.Models;
using DentalApp.Web.Endpoints.Appointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DentalApp.Web.Pages.Appointments;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly IAppointmentsApiClient _api;
    public IndexModel(IAppointmentsApiClient api) => _api = api;

    public List<AppointmentDto> Appointments { get; set; } = new();

    public async Task OnGetAsync()
    {
        var list = await _api.GetMineAsync();
        Appointments = list.Select(a => new AppointmentDto
        {
            Id = a.Id,
            DoctorId = a.DoctorId,
            UserName = a.UserName,
            DoctorName = a.DoctorName,
            ProcedureName = a.ProcedureName,
            ProcedureId = a.ProcedureId,
            StartTime = a.StartTime,
            EndTime = a.EndTime,
            Status = a.Status
        }).ToList();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _api.DeleteAsync(id);
        TempData["SuccessMessage"] = "Appointment deleted successfully.";
        return RedirectToPage();
    }
}
