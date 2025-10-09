using DentalApp.Application.Appointments.Commands.DeleteAppointment;
using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DentalApp.Web.Pages.Appointments;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(IMediator mediator, IApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _context = context;
        _userManager = userManager;
    }

    public List<Appointment> Appointments { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = _userManager.GetUserId(User);
        var isAdmin = User.IsInRole("Admin");

        IQueryable<Appointment> query = _context.Appointments
            .Include(a => a.User)
            .Include(a => a.Doctor)
            .Include(a => a.Procedure)
            .AsNoTracking();

        if (!isAdmin)
        {
            query = query.Where(a => a.UserId == userId);
        }

        Appointments = await query.ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _mediator.Send(new DeleteAppointmentCommand { Id = id });
        return RedirectToPage();
    }
}
