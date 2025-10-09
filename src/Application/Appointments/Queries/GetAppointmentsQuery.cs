using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Appointments.Queries;

public record GetAppointmentsQuery : IRequest<List<AppointmentDto>>
{
    public string? UserId { get; init; } 
    public int? PageNumber { get; init; }
    public int? PageSize { get; init; }
}
