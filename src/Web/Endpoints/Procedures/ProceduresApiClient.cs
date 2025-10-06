
namespace DentalApp.Web.Endpoints.Procedures;

public class ProceduresApiClient : IProceduresApiClient
{
    private readonly IHttpClientFactory _factory;
    private readonly IHttpContextAccessor _http;

    public ProceduresApiClient(IHttpClientFactory factory, IHttpContextAccessor http)
    {
        _factory = factory;
        _http = http;
    }

    private HttpClient CreateClient() => _factory.CreateClient();

    private string BaseUrl =>
        $"{_http.HttpContext!.Request.Scheme}://{_http.HttpContext!.Request.Host}";

    public async Task<List<ProcedureResponse>> GetAllAsync(CancellationToken ct = default)
    {
        var http = CreateClient();
        var url = $"{BaseUrl}/api/procedures";
        var data = await http.GetFromJsonAsync<List<ProcedureResponse>>(url, ct);
        return data ?? new List<ProcedureResponse>();
    }

    public async Task<ProcedureResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var http = CreateClient();
        var url = $"{BaseUrl}/api/procedures/{id}";
        var data = await http.GetFromJsonAsync<ProcedureResponse>(url, ct);
        if (data is null) throw new KeyNotFoundException("Procedure not found.");
        return data;
    }

    public async Task CreateAsync(CreateProcedureRequest req, CancellationToken ct = default)
    {
        var http = CreateClient();
        var url = $"{BaseUrl}/api/procedures";
        var resp = await http.PostAsJsonAsync(url, req, ct);
        resp.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(int id, UpdateProcedureRequest req, CancellationToken ct = default)
    {
        var http = CreateClient();
        var url = $"{BaseUrl}/api/procedures/{id}";
        var resp = await http.PutAsJsonAsync(url, req, ct);
        resp.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var http = CreateClient();
        var url = $"{BaseUrl}/api/procedures/{id}";
        var resp = await http.DeleteAsync(url, ct);
        resp.EnsureSuccessStatusCode();
    }
}

