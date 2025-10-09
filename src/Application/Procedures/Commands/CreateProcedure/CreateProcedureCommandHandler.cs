using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;

namespace DentalApp.Application.Procedures.Commands.CreateProcedure;
public class CreateProcedureCommandHandler : IRequestHandler<CreateProcedureCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProcedureCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProcedureCommand request, CancellationToken cancellationToken)
    {
        var entity = new Procedure
        {
            ProcedureName = request.ProcedureName,
            Duration = request.Duration,
            Price = request.Price
        };

        _context.Procedures.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
