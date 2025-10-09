using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Appointments.Queries;
public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, List<AppointmentDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAppointmentsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AppointmentDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var q = _context.Appointments
            .AsNoTracking()
            .Include(a => a.User)
            .Include(a => a.Doctor)
            .Include(a => a.Procedure)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.UserId))
        {
            q = q.Where(a => a.UserId == request.UserId);
        }

        q = q.OrderByDescending(a => a.StartTime);

        var dtos = await q.Select(a => new AppointmentDto
        {
            Id = a.Id,
            UserId = a.UserId,
            UserName = (a.User.FirstName + " " + a.User.LastName).Trim(),
            DoctorId = a.DoctorId,
            DoctorName = a.Doctor.FullName,
            ProcedureId = a.ProcedureId,
            ProcedureName = a.Procedure.ProcedureName,
            StartTime = a.StartTime,
            EndTime = a.EndTime,
            Status = a.Status.ToString()
        }).ToListAsync(cancellationToken);

        return dtos;
    }
}
