using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;

namespace DentalApp.Application.Appointments.Queries.GetAppointmentWithPagination;

public record GetAppointmentsQuery : IRequest<List<Appointment>>;

public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, List<Appointment>>
{
    private readonly IApplicationDbContext _context;

    public GetAppointmentsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Appointment>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Appointments
            .Include(a => a.User)
            .Include(a => a.Doctor)
            .Include(a => a.Procedure)
            .ToListAsync(cancellationToken);
    }
}
