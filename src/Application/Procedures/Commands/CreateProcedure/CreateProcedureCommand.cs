using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;

public class CreateProcedureCommand : IRequest<int> 
{
    public string ProcedureName { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public int Price { get; set; }
}
