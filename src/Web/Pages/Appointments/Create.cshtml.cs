using DentalApp.Application.Appointments.Commands.CreateAppointment;
using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Security;
using DentalApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Web.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(IApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mediator = mediator;
            _userManager = userManager;
        }

        [BindProperty]
        public CreateAppointmentCommand Appointment { get; set; } = new();

        public List<Doctor> DoctorsList { get; set; } = new();
        public List<Procedure> ProceduresList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (User?.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = "/Appointments/Create" });
            }

            DoctorsList = await _context.Doctors.ToListAsync();
            ProceduresList = await _context.Procedures.ToListAsync();

            if (!DoctorsList.Any() || !ProceduresList.Any())
            {
                ViewData["ErrorMessage"] = "Doctors or Procedures are not available yet.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User?.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = "/Appointments/Create" });
            }

            DoctorsList = await _context.Doctors.ToListAsync();
            ProceduresList = await _context.Procedures.ToListAsync();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validare StartTime între 08:00 și 17:00
            var startHour = Appointment.StartTime.Hour;
            if (startHour < 8 || startHour >= 17)
            {
                ModelState.AddModelError("Appointment.StartTime", "Appointments can only be scheduled between 08:00 and 17:00.");
                return Page();
            }

            // Preluam procedura selectată pentru durata
            var procedure = await _context.Procedures.FirstOrDefaultAsync(p => p.Id == Appointment.ProcedureId);
            if (procedure == null)
            {
                ModelState.AddModelError(string.Empty, "Selected procedure not found.");
                return Page();
            }

            // Cream command cu EndTime calculat și UserId din utilizatorul logat
            var command = new CreateAppointmentCommand
            {
                UserId = _userManager.GetUserId(User), 
                DoctorId = Appointment.DoctorId,
                ProcedureId = Appointment.ProcedureId,
                StartTime = Appointment.StartTime,
                EndTime = Appointment.StartTime.Add(procedure.Duration)
            };

            var newId = await _mediator.Send(command);

            return RedirectToPage("/Index"); 
        }
    }
}
