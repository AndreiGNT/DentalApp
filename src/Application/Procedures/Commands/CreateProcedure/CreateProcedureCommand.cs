namespace DentalApp.Application.Procedures.Commands.CreateProcedure;   

public record CreateProcedureCommand(string ProcedureName, TimeSpan Duration, int Price) : IRequest<int>;
