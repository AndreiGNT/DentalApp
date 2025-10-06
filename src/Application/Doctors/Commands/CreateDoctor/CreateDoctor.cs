using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;

namespace DentalApp.Application.Doctors.Commands.CreateDoctor;

public record CreateDoctorCommand(string FullName, List<int> ProcedureIds) : IRequest<int>;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateDoctorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        if (request.ProcedureIds.Count > 3)
        {
            throw new InvalidOperationException("A doctor can be assigned to a maximum of 3 procedures.");
        }

        var doctor = new Doctor
        {
            FullName = request.FullName,
            DoctorProcedures = request.ProcedureIds
                .Select(pId => new DoctorProcedure { ProcedureId = pId })
                .ToList()
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync(cancellationToken);

        return doctor.Id;
    }
}
