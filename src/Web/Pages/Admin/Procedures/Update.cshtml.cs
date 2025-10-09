using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Procedures.Commands.UpdateProcedure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Web.Pages.Admin.Procedures
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
        public string ProcedureName { get; set; } = string.Empty;

        [BindProperty]
        public int Price { get; set; }

        [BindProperty]
        public TimeSpan Duration { get; set; }

        public List<SelectListItem> DurationOptions { get; private set; } = new();

        private void BuildDurationOptions(int minMinutes = 15, int maxMinutes = 180, int step = 15)
        {
            DurationOptions = new List<SelectListItem>();
            for (int m = minMinutes; m <= maxMinutes; m += step)
            {
                var ts = TimeSpan.FromMinutes(m);
                DurationOptions.Add(new SelectListItem
                {
                    Text = ts.ToString(@"hh\:mm"),
                    
                    Value = ts.ToString(@"hh\:mm\:ss")
                });
            }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BuildDurationOptions();

            var procedure = await _context.Procedures
                .FirstOrDefaultAsync(d => d.Id == id);

            if (procedure == null)
            {
                return NotFound();
            }

            Id = procedure.Id;
            ProcedureName = procedure.ProcedureName;
            Price = procedure.Price;
            Duration = procedure.Duration;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                BuildDurationOptions();
                return Page();
            }

            try
            {
                await _mediator.Send(new UpdateProcedureCommand
                {
                    Id = Id,
                    ProcedureName = ProcedureName,
                    Price = Price,
                    Duration = Duration
                });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage("/Admin/Procedures/Index");
        }
    }
}
