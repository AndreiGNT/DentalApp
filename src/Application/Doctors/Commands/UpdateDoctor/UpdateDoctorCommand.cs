namespace DentalApp.Application.Doctors.Commands.UpdateDoctor;

public record UpdateDoctorCommand : IRequest
{
    public int Id { get; init; }

    public string FullName { get; init; } = string.Empty;

    public List<int> ProcedureIds { get; init; } = new List<int>();

}


