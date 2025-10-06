using System.ComponentModel.DataAnnotations;
using DentalApp.Web.Endpoints.Procedures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DentalApp.Web.Pages.Admin.Procedures
{
    public class UpdateModel : PageModel
    {
        private readonly IProceduresApiClient _api;
        public UpdateModel(IProceduresApiClient api) { _api = api; }

        [BindProperty] public UpdateProcedureForm Form { get; set; } = new();

        public async Task OnGetAsync(int id)
        {
            var dto = await _api.GetByIdAsync(id);
            Form = new UpdateProcedureForm
            {
                Id = dto.Id,
                ProcedureName = dto.ProcedureName,
                Duration = dto.Duration,
                Price = dto.Price
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var req = new UpdateProcedureRequest(
                ProcedureName: Form.ProcedureName,
                Duration: Form.Duration,
                Price: Form.Price
            );

            await _api.UpdateAsync(Form.Id, req);
            TempData["SuccessMessage"] = "Procedure updated successfully.";
            return RedirectToPage("Index");
        }

        public class UpdateProcedureForm
        {
            public int Id { get; set; }

            [Required, StringLength(200)]
            public string ProcedureName { get; set; } = string.Empty;

            [Required]
            public TimeSpan Duration { get; set; }

            [Range(0, int.MaxValue)]
            public int Price { get; set; }
        }
    }
}
