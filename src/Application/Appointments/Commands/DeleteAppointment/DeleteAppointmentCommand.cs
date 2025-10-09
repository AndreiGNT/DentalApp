
namespace DentalApp.Application.Appointments.Commands.DeleteAppointment;

public record DeleteAppointmentCommand : IRequest
{
    public int Id { get; init; }
}
