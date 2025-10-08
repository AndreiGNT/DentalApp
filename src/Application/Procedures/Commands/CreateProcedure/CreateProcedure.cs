using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;

public class CreateProcedure : IRequest<int> 
{
    public string ProcedureName { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public int Price { get; set; }

    public class CreateProcedureHandler : IRequestHandler<CreateProcedure, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateProcedureHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProcedure request, CancellationToken cancellationToken)
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
}
