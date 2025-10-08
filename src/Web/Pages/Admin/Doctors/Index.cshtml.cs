using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Procedures.Commands.DeleteProcedure;
using DentalApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Web.Pages.Admin.Doctors;

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

    public List<Doctor> Doctors { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        Doctors = await _context.Doctors
                .Include(d => d.DoctorProcedures)
                .ThenInclude(dp => dp.Procedure)
                .AsNoTracking()
                .ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _mediator.Send(new DeleteDoctorCommand { Id = id });
        return RedirectToPage();
    }
}
