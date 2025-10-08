using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Procedures.Queries;

public class GetProcedureById : IRequest<ProcedureDto>
{
    public int Id { get; set; }
}

public class GetProcedureByIdHandler : IRequestHandler<GetProcedureById, ProcedureDto>
{
    private readonly IApplicationDbContext _context;

    public GetProcedureByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProcedureDto> Handle(GetProcedureById request, CancellationToken cancellationToken)
    {
        var procedure = await _context.Procedures
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (procedure == null)
            throw new KeyNotFoundException("Procedure not found.");

        return new ProcedureDto
        {
            Id = procedure.Id,
            ProcedureName = procedure.ProcedureName,
            Duration = procedure.Duration,
            Price = procedure.Price
        };
    }
}
