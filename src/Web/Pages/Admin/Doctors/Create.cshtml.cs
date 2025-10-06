using DentalApp.Application.Doctors.Commands.CreateDoctor;
using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Web.Pages.Admin.Doctors
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;

        public CreateModel(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [BindProperty]
        public string FullName { get; set; } = string.Empty;

        [BindProperty]
        public List<int> SelectedProcedures { get; set; } = new();

        public List<Procedure> AvailableProcedures { get; set; } = new();

        public async Task OnGetAsync()
        {
            AvailableProcedures = await _context.Procedures
                .OrderBy(p => p.ProcedureName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // reîncãrcam lista procedurilor
                return Page();
            }

            var command = new CreateDoctorCommand(FullName, SelectedProcedures);
            var id = await _mediator.Send(command);

            return RedirectToPage("/Admin/Doctors/Index");
        }
    }
}
