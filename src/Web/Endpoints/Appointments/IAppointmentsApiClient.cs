namespace DentalApp.Web.Endpoints.Appointments;

public interface IAppointmentsApiClient
{
    Task<List<AppointmentResponse>> GetAllAsync(CancellationToken ct = default);

    Task<List<AppointmentResponse>> GetMineAsync(CancellationToken ct = default);

    Task<AppointmentResponse> GetByIdAsync(int id, CancellationToken ct = default);

    Task<int> CreateAsync(CreateAppointmentRequest req, CancellationToken ct = default);

    Task UpdateAsync(int id, UpdateAppointmentRequest req, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);
}
