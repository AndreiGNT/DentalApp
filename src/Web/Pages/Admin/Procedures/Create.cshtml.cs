using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Procedures.Commands.CreateProcedure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace DentalApp.Web.Pages.Admin.Procedures
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

        public  Task OnGetAsync() 
        {
            BuildDurationOptions();

            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            var command = new CreateProcedureCommand(ProcedureName, Duration, Price);
            var id = await _mediator.Send(command);

            return RedirectToPage("/Admin/Procedures/Index");
        }
    }
}
