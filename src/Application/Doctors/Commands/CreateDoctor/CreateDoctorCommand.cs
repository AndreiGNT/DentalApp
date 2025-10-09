namespace DentalApp.Application.Doctors.Commands.CreateDoctor;

public record CreateDoctorCommand(string FullName, List<int> ProcedureIds) : IRequest<int>;
