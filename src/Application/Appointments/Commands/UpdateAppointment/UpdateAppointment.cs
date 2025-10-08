using DentalApp.Application.Common.Interfaces;

namespace DentalApp.Application.Appointments.Commands.UpdateAppointment;

public record UpdateAppointmentCommand(
        int Id,
        string? UserId = null,
        int? DoctorId = null,
        int? ProcedureId = null,
        DateTime? StartTime = null,
        DateTime? EndTime = null,
        string? Status = null
    ) : IRequest;

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException("Appointment not found.");

        if (!string.IsNullOrEmpty(request.UserId)) entity.UserId = request.UserId;
        if (request.DoctorId.HasValue) entity.DoctorId = request.DoctorId.Value;
        if (request.ProcedureId.HasValue) entity.ProcedureId = request.ProcedureId.Value;
        if (request.StartTime.HasValue) entity.StartTime = request.StartTime.Value;
        if (request.EndTime.HasValue) entity.EndTime = request.EndTime.Value;
        if (!string.IsNullOrEmpty(request.Status)) entity.Status = request.Status;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
