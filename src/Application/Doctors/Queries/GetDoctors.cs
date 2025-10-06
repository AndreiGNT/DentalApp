using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Doctors.Queries;

public record GetDoctorsQuery : IRequest<List<DoctorDto>>;

public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, List<DoctorDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDoctorsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DoctorDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _context.Doctors
                .Include(d => d.DoctorProcedures)
                .ThenInclude(dp => dp.Procedure)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

        return doctors.Select(d => new DoctorDto
        {
            Id = d.Id,
            FullName = d.FullName,
            Procedures = d.DoctorProcedures
                .Where(dp => dp.Procedure != null)
                .Select(dp => dp.Procedure!.ProcedureName)
                .ToList()
        }).ToList();
    }
}
