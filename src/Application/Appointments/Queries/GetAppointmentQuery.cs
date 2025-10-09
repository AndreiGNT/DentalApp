using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Appointments.Queries;

public record GetAppointmentQuery : IRequest<AppointmentDto>
{
    public int Id { get; init; }
}
