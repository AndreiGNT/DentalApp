using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Appointments.Queries;
public class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, AppointmentDto?>
{
    private readonly IApplicationDbContext _context;

    public GetAppointmentQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AppointmentDto?> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Appointments
            .Include(a => a.User)
            .Include(a => a.Doctor)
            .Include(a => a.Procedure)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        return new AppointmentDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            UserName = $"{entity.User?.FirstName} {entity.User?.LastName}",
            DoctorId = entity.DoctorId,
            DoctorName = entity.Doctor?.FullName ?? string.Empty,
            ProcedureId = entity.ProcedureId,
            ProcedureName = entity.Procedure?.ProcedureName ?? string.Empty,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Status = entity.Status
        };
    }
}
