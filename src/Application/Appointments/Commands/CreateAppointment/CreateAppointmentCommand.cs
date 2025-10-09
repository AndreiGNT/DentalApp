
namespace DentalApp.Application.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand(
    string? UserId, 
    int DoctorId,
    int ProcedureId,
    DateTime StartTime,
    DateTime EndTime
    ) : IRequest<int>;
