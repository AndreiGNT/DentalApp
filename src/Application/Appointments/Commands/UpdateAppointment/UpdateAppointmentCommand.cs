
namespace DentalApp.Application.Appointments.Commands.UpdateAppointment;

public record UpdateAppointmentCommand(
        int Id,
        string? UserId = null,
        int? DoctorId = null,
        int? ProcedureId = null,
        DateTime? StartTime = null,
        DateTime? EndTime = null,
        string? Status = null
    ) : IRequest;
