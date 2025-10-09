using DentalApp.Application.Common.Models;
using DentalApp.Web.Endpoints.Procedures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DentalApp.Web.Pages.Admin.Procedures
{
    public class UpdateModel : PageModel
    {
        private readonly IProceduresApiClient _api;
        public UpdateModel(IProceduresApiClient api) { _api = api; }

        [BindProperty] public ProcedureDto Form { get; set; } = new();

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

        public async Task OnGetAsync(int id)
        {
            BuildDurationOptions();

            var dto = await _api.GetByIdAsync(id);

            Form = new ProcedureDto
            {
                Id = dto.Id,
                ProcedureName = dto.ProcedureName,
                Duration = dto.Duration,
                Price = dto.Price
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                BuildDurationOptions();
                return Page();
            }

            var req = new UpdateProcedureRequest(
                ProcedureName: Form.ProcedureName,
                Duration: Form.Duration,
                Price: Form.Price
            );

            await _api.UpdateAsync(Form.Id, req);
            TempData["SuccessMessage"] = "Procedure updated successfully.";
            return RedirectToPage("Index");
        }

    }
}
