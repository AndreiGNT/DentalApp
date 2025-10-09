using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Procedures.Queries;

public class GetProcedureQuery : IRequest<ProcedureDto>
{
    public int Id { get; set; }
}
