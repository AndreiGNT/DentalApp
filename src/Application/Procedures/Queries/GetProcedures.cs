using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Procedures.Queries;

public class GetProcedures : IRequest<List<ProcedureDto>>
{
}

public class GetProceduresHandler : IRequestHandler<GetProcedures, List<ProcedureDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProceduresHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProcedureDto>> Handle(GetProcedures request, CancellationToken cancellationToken)
    {
        return await _context.Procedures
            .AsNoTracking()
            .Select(p => new ProcedureDto
            {
                Id = p.Id,
                ProcedureName = p.ProcedureName,
                Duration = p.Duration,
                Price = p.Price
            })
            .ToListAsync(cancellationToken);
    }
}
