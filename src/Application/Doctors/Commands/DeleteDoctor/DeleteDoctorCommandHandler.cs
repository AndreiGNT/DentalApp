using DentalApp.Application.Common.Interfaces;

namespace DentalApp.Application.Doctors.Commands.DeleteDoctor;

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteDoctorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (doctor == null)
        {
            throw new KeyNotFoundException("Doctor not found.");
        }
            
        _context.DoctorProcedures.RemoveRange(doctor.DoctorProcedures);

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
