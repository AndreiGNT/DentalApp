using DentalApp.Web.Endpoints.Appointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DentalApp.Web.Pages.Appointments;

//[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly IAppointmentsApiClient _api;
    public IndexModel(IAppointmentsApiClient api) => _api = api;

    public List<AppointmentVm> Appointments { get; set; } = new();

    public async Task OnGetAsync()
    {
        //var list = await _api.GetMineAsync();
        var list = await _api.GetAllAsync();
        Appointments = list.Select(a => new AppointmentVm
        {
            Id = a.Id,
            DoctorId = a.DoctorId,
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

    public class AppointmentVm
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int DoctorId { get; set; }
        public int ProcedureId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Status { get; set; }
    }
}
