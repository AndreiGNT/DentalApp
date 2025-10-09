
namespace DentalApp.Application.Procedures.Commands.UpdateProcedure;

public class UpdateProcedureCommand : IRequest
{
    public int Id { get; set; }
    public string ProcedureName { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public int Price { get; set; }   
}
