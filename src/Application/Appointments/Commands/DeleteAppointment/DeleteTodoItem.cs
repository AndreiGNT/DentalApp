using DentalApp.Application.Common.Interfaces;

namespace DentalApp.Application.Appointments.Commands.DeleteAppointment;

public record DeleteAppointmentCommand(int Id) : IRequest;

public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException($"Appointment with Id {request.Id} not found.");

        _context.Appointments.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
