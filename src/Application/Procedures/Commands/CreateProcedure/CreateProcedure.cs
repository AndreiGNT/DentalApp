
using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;

namespace Application.Procedures.Commands.CreateProcedure
{
    public class CreateProcedure : IRequest
    {
        public string ProcedureName { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public int Price { get; set; }

        public class Handler : IRequestHandler<CreateProcedure>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(CreateProcedure request, CancellationToken cancellationToken)
            {
                var entity = new Procedure
                {
                    ProcedureName = request.ProcedureName,
                    Duration = request.Duration,
                    Price = request.Price
                };

                _context.Procedures.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

            }
        }
    }
}
