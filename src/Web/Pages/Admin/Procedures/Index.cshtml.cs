using DentalApp.Web.Endpoints.Procedures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DentalApp.Web.Pages.Admin.Procedures
{
    public class IndexModel : PageModel
    {
        private readonly IProceduresApiClient _api;
        public IndexModel(IProceduresApiClient api) { _api = api; }

        public List<ProcedureVm> Procedures { get; set; } = new();

        public async Task OnGetAsync()
        {
            var list = await _api.GetAllAsync();
            Procedures = list.Select(p => new ProcedureVm
            {
                Id = p.Id,
                ProcedureName = p.ProcedureName,
                Duration = p.Duration,
                Price = p.Price
            }).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _api.DeleteAsync(id);
            TempData["SuccessMessage"] = "Procedure deleted successfully.";
            return RedirectToPage();
        }

        public class ProcedureVm
        {
            public int Id { get; set; }
            public string ProcedureName { get; set; } = string.Empty;
            public TimeSpan Duration { get; set; }
            public int Price { get; set; }
        }
    }
}
