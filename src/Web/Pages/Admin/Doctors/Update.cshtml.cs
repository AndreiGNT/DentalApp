using DentalApp.Application.Doctors.Commands.UpdateDoctor;
using DentalApp.Domain.Entities;
using DentalApp.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Web.Pages.Admin.Doctors
{
    public class UpdateModel : PageModel
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public UpdateModel(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string FullName { get; set; } = string.Empty;

        [BindProperty]
        public List<int> SelectedProcedures { get; set; } = new();

        public List<Procedure> AllProcedures { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.DoctorProcedures)
                .ThenInclude(dp => dp.Procedure)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                return NotFound();

            Id = doctor.Id;
            FullName = doctor.FullName;
            SelectedProcedures = doctor.DoctorProcedures.Select(dp => dp.ProcedureId).ToList();
            AllProcedures = await _context.Procedures.AsNoTracking().ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                await _mediator.Send(new UpdateDoctorCommand
                {
                    Id = Id,
                    FullName = FullName,
                    ProcedureIds = SelectedProcedures
                });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                AllProcedures = await _context.Procedures.AsNoTracking().ToListAsync();
                return Page();
            }

            return RedirectToPage("/Admin/Doctors/Index");
        }
    }
}
