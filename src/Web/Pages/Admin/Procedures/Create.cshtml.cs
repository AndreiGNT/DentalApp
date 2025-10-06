using System.ComponentModel.DataAnnotations;
using DentalApp.Web.Endpoints.Procedures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DentalApp.Web.Pages.Admin.Procedures
{
    public class CreateModel : PageModel
    {
        private readonly IProceduresApiClient _api;
        public CreateModel(IProceduresApiClient api) { _api = api; }

        [BindProperty] public CreateProcedureForm Form { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

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
