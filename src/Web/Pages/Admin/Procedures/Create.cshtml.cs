using System.ComponentModel.DataAnnotations;
using DentalApp.Web.Endpoints.Procedures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace DentalApp.Web.Pages.Admin.Procedures
{
    public class CreateModel : PageModel
    {
        private readonly IProceduresApiClient _api;
        public CreateModel(IProceduresApiClient api) { _api = api; }

        [BindProperty] public CreateProcedureForm Form { get; set; } = new();

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

        public Task OnGet() 
        {
            BuildDurationOptions();
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                BuildDurationOptions();
                return Page();
            }

            var req = new CreateProcedureRequest(
                ProcedureName: Form.ProcedureName,
                Duration: Form.Duration,
                Price: Form.Price
            );

            await _api.CreateAsync(req);
            TempData["SuccessMessage"] = "Procedure created successfully.";
            return RedirectToPage("Index");
        }

        public class CreateProcedureForm
        {
            [Required, StringLength(200)]
            public string ProcedureName { get; set; } = string.Empty;

            [Required]
            public TimeSpan Duration { get; set; }

            [Range(0, int.MaxValue)]
            public int Price { get; set; }
        }
    }
}
