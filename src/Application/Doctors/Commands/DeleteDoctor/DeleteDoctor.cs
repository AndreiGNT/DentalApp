using DentalApp.Application.Common.Interfaces;

namespace DentalApp.Application.Doctors.Commands.DeleteDoctor;

public class DeleteProcedure : IRequest
{
    public int Id { get; set; }

    public class DeleteProcedureHandler : IRequestHandler<DeleteProcedure>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProcedureHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProcedure request, CancellationToken cancellationToken)
        {
            var procedure = await _context.Procedures
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (procedure == null)
                throw new KeyNotFoundException("Procedure not found.");

            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
