
using DentalApp.Application.Common.Interfaces;

namespace Application.Procedures.Commands.UpdateProcedure
{
    public class UpdateProcedure : IRequest
    {
        public int Id { get; set; }
        public string ProcedureName { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public int Price { get; set; }

        public class Handler : IRequestHandler<UpdateProcedure>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateProcedure request, CancellationToken cancellationToken)
            {
                var procedure = await _context.Procedures
                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                if (procedure == null)
                    throw new KeyNotFoundException("Procedure not found.");

                procedure.ProcedureName = request.ProcedureName;
                procedure.Duration = request.Duration;
                procedure.Price = request.Price;

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
