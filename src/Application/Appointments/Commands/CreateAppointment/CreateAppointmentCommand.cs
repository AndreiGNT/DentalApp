
namespace DentalApp.Application.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand : IRequest<int>
{
    public string? UserId { get; init; }
    public int DoctorId { get; init; }
    public int ProcedureId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
}
