using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Security;
using DentalApp.Application.Procedures.Commands.DeleteProcedure;
using DentalApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Web.Pages.Admin.Procedures;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator, IApplicationDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public List<Procedure> Procedures { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        Procedures = await _context.Procedures
                .AsNoTracking()
                .ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _mediator.Send(new DeleteProcedureCommand { Id = id });
        return RedirectToPage();
    }
}
