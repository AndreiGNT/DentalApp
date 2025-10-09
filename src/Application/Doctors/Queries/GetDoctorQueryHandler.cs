using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Doctors.Queries;

public class GetDoctorQueryHandler : IRequestHandler<GetDoctorQuery, DoctorDto?>
{
    private readonly IApplicationDbContext _context;

    public GetDoctorQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorDto?> Handle(GetDoctorQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
                .Include(d => d.DoctorProcedures)
                .ThenInclude(dp => dp.Procedure)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (doctor == null)
        {
            throw new KeyNotFoundException("Doctor not found.");
        }

        return new DoctorDto
        {
            Id = doctor.Id,
            FullName = doctor.FullName,
            Procedures = doctor.DoctorProcedures
                .Where(dp => dp.Procedure != null)
                .Select(dp => dp.Procedure!.ProcedureName)
                .ToList()
        };
    }
}
