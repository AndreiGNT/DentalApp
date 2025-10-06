
namespace DentalApp.Web.Endpoints.Procedures;

public interface IProceduresApiClient
{
    Task<List<ProcedureResponse>> GetAllAsync(CancellationToken ct = default);

    Task<ProcedureResponse> GetByIdAsync(int id, CancellationToken ct = default);

    Task CreateAsync(CreateProcedureRequest req, CancellationToken ct = default);

    Task UpdateAsync(int id, UpdateProcedureRequest req, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);
}
