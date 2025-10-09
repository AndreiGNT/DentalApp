using DentalApp.Application.Common.Interfaces;

namespace DentalApp.Application.Appointments.Commands.DeleteAppointment;
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
        {
            throw new KeyNotFoundException("Appointment not found.");
        }
            
        _context.Appointments.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
