namespace DentalApp.Web.Endpoints.Appointments;

public record AppointmentResponse(
    int Id,
    string? UserId,
    int DoctorId,
    int ProcedureId,
    DateTime StartTime,
    DateTime EndTime,
    string? Status 
);

public record CreateAppointmentRequest(
    string? UserId,
    int DoctorId,
    int ProcedureId,
    DateTime StartTime
);

public record UpdateAppointmentRequest(
    int Id,
    string? UserId,
    int DoctorId,
    int ProcedureId,
    DateTime StartTime,
    DateTime EndTime,
    string? Status
);
