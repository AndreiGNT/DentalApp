
namespace DentalApp.Web.Endpoints.Appointments;

public class AppointmentsApiClient : IAppointmentsApiClient
{
    private readonly IHttpClientFactory _factory;

    private readonly IHttpContextAccessor _http;

    private HttpClient Client => _factory.CreateClient();

    private string BaseUrl => $"{_http.HttpContext!.Request.Scheme}://{_http.HttpContext!.Request.Host}";

    public AppointmentsApiClient(IHttpClientFactory factory, IHttpContextAccessor http)
    {
        _factory = factory;
        _http = http;
    }

    private HttpRequestMessage WithAuth(HttpRequestMessage req)
    {
        var ctx = _http.HttpContext!;

        if (ctx.Request.Headers.TryGetValue("Cookie", out var c))
        {
            req.Headers.TryAddWithoutValidation("Cookie", c.ToString());
        }
            
        if (ctx.Request.Headers.TryGetValue("Authorization", out var a))
        {
            req.Headers.TryAddWithoutValidation("Authorization", a.ToString());
        }

        return req;
    }

    public async Task<List<AppointmentResponse>> GetAllAsync(CancellationToken ct = default)
    {
        var req = WithAuth(new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/api/appointments"));
        var resp = await Client.SendAsync(req, ct);
        resp.EnsureSuccessStatusCode();

        return 
            await resp.Content.ReadFromJsonAsync<List<AppointmentResponse>>(cancellationToken: ct)
            ?? new();
    }

    public async Task<List<AppointmentResponse>> GetMineAsync(CancellationToken ct = default)
    {
        var req = WithAuth(new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/api/appointments/mine"));
        var resp = await Client.SendAsync(req, ct);
        resp.EnsureSuccessStatusCode();

        return 
            await resp.Content.ReadFromJsonAsync<List<AppointmentResponse>>(cancellationToken: ct) 
            ?? new();
    }


    public async Task<AppointmentResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var req = WithAuth(new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/api/appointments/{id}"));
        var resp = await Client.SendAsync(req, ct);
        resp.EnsureSuccessStatusCode();

        return (await resp.Content.ReadFromJsonAsync<AppointmentResponse>(cancellationToken: ct))!;
    }

    public async Task<int> CreateAsync(CreateAppointmentRequest model, CancellationToken ct = default)
    {
        var req = WithAuth(new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/api/appointments")
        {
            Content = JsonContent.Create(model)
        });

        var resp = await Client.SendAsync(req, ct);
        resp.EnsureSuccessStatusCode();
        var id = await resp.Content.ReadFromJsonAsync<int>(cancellationToken: ct);

        return id;
    }

    public async Task UpdateAsync(int id, UpdateAppointmentRequest model, CancellationToken ct = default)
    {
        var req = WithAuth(new HttpRequestMessage(HttpMethod.Put, $"{BaseUrl}/api/appointments/{id}")
        {
            Content = JsonContent.Create(model)
        });

        var resp = await Client.SendAsync(req, ct);
        resp.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var req = WithAuth(new HttpRequestMessage(HttpMethod.Delete, $"{BaseUrl}/api/appointments/{id}"));
        var resp = await Client.SendAsync(req, ct);
        resp.EnsureSuccessStatusCode();
    }
}
