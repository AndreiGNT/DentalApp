using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Statistics.Queries;

public class GetAppointmentStatisticsHandler : IRequestHandler<GetAppointmentStatisticsQuery, List<AppointmentStatisticsDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAppointmentStatisticsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AppointmentStatisticsDto>> Handle(GetAppointmentStatisticsQuery request, CancellationToken cancellationToken)
    {
        var statistics = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Procedure)
            .GroupBy(a => new { a.Doctor.FullName, a.Procedure.ProcedureName })
            .Select(g => new AppointmentStatisticsDto
            {
                DoctorName = g.Key.FullName,
                ProcedureName = g.Key.ProcedureName,
                AppointmentCount = g.Count()
            })
            .OrderByDescending(s => s.AppointmentCount)
            .ToListAsync(cancellationToken);

        return statistics;
    }
}

