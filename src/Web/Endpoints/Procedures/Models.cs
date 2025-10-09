namespace DentalApp.Web.Endpoints.Procedures;

public record ProcedureResponse(
    int Id, 
    string ProcedureName, 
    TimeSpan Duration, 
    int Price
    );

public record CreateProcedureRequest(
    string ProcedureName, 
    TimeSpan Duration, 
    int Price
    );

public record UpdateProcedureRequest(
    string ProcedureName, 
    TimeSpan Duration, 
    int Price
    );

