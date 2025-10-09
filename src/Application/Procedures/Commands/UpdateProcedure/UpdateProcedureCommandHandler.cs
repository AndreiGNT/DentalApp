using DentalApp.Application.Common.Interfaces;

namespace DentalApp.Application.Procedures.Commands.UpdateProcedure;
public class UpdateProcedureCommandHandler : IRequestHandler<UpdateProcedureCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProcedureCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProcedureCommand request, CancellationToken cancellationToken)
    {
        var procedure = await _context.Procedures
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (procedure == null)
        {
            throw new KeyNotFoundException("Procedure not found.");
        }

        procedure.ProcedureName = request.ProcedureName;
        procedure.Duration = request.Duration;
        procedure.Price = request.Price;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
