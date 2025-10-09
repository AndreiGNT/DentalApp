using DentalApp.Application.Appointments.Commands.CreateAppointment;
using DentalApp.Application.Common.Interfaces;
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

        public string? UserId { get; set; }

        [BindProperty]
        public int DoctorId { get; set; }

        [BindProperty]
        public int ProcedureId { get; set; }

        [BindProperty]
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


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

            // Validare StartTime

            var startDate = StartTime.Date;
            if (startDate <= DateTime.Today)
            {
                ModelState.AddModelError("StartTime", "Appointments must be scheduled at least one day in advance.");
                return Page();
            }

            var startHour = StartTime.Hour;
            if (startHour < 8 || startHour > 17)
            {
                ModelState.AddModelError("StartTime", "Appointments can only be scheduled between 08:00 and 17:00.");
                return Page();
            }

            // Preluam procedura selectată pentru durata
            var procedure = await _context.Procedures.FirstOrDefaultAsync(p => p.Id == ProcedureId);
            if (procedure == null)
            {
                ModelState.AddModelError(string.Empty, "Selected procedure not found.");
                return Page();
            }

            UserId = _userManager.GetUserId(User);
            EndTime = StartTime.Add(procedure.Duration);

            // Cream command cu EndTime calculat și UserId din utilizatorul logat
            var command = new CreateAppointmentCommand(
            
                UserId, 
                DoctorId,
                ProcedureId,
                StartTime,
                EndTime
            );

            var newId = await _mediator.Send(command);

            return RedirectToPage("/Index"); 
        }
    }
}
