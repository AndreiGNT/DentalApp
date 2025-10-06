using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Doctors.Commands.DeleteDoctor;
using DentalApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Application.Doctors.Commands.UpdateDoctor;

public record UpdateDoctorCommand : IRequest
{
    public int Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public List<int> ProcedureIds { get; init; } = new List<int>();
}

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateDoctorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .Include(d => d.DoctorProcedures)
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (doctor == null)
            throw new KeyNotFoundException("Doctor not found.");

        if (request.ProcedureIds.Count > 3)
            throw new InvalidOperationException("A doctor can be assigned to a maximum of 3 procedures.");

        doctor.FullName = request.FullName;

        doctor.DoctorProcedures.Clear();
        foreach (var pid in request.ProcedureIds)
        {
            doctor.DoctorProcedures.Add(new DoctorProcedure { ProcedureId = pid, DoctorId = doctor.Id });
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}


