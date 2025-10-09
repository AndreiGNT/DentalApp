using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;

namespace DentalApp.Application.Procedures.Commands.DeleteProcedure;

public class DeleteProcedureCommandHandler : IRequestHandler<DeleteProcedureCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProcedureCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProcedureCommand request, CancellationToken cancellationToken)
    {
        var procedure = await _context.Procedures
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (procedure == null)
        {
            throw new KeyNotFoundException("Procedure not found.");
        }

        //_context.DoctorProcedures.RemoveRange(procedure.DoctorProcedures);

        _context.Procedures.Remove(procedure);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
