using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;
using DentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Application.Appointments.Queries.GetAppointmentWithPagination;

public record GetAppointmentsQuery : IRequest<List<AppointmentDto>>;

public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, List<AppointmentDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAppointmentsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AppointmentDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _context.Appointments
            .Include(a => a.User)
            .Include(a => a.Doctor)
            .Include(a => a.Procedure)
            .ToListAsync(cancellationToken);

        var dtos = appointments.Select(a => new AppointmentDto
        {
            Id = a.Id,
            UserId = a.UserId,
            UserName = $"{a.User.FirstName} {a.User.LastName}",
            DoctorId = a.DoctorId,
            DoctorName = $"{a.Doctor.FullName}",
            ProcedureId = a.ProcedureId,
            ProcedureName = a.Procedure.ProcedureName,
            StartTime = a.StartTime,
            EndTime = a.EndTime,
            Status = a.Status.ToString()
        }).ToList();

        return dtos;
    }
}
