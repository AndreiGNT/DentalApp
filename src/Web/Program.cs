using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;
using DentalApp.Infrastructure;
using DentalApp.Infrastructure.Data;
using DentalApp.Web.Endpoints;
using DentalApp.Web.Endpoints.Appointments;
using DentalApp.Web.Endpoints.Procedures;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddKeyVaultIfConfigured();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services
    //.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy =>
        policy.RequireRole("Admin"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IProceduresApiClient, ProceduresApiClient>();
builder.Services.AddScoped<IAppointmentsApiClient, AppointmentsApiClient>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();

    app.UseSwaggerUi(settings =>
    {
        settings.Path = "/api";
        settings.DocumentPath = "/api/specification.json";
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); 

app.UseAuthentication();  
app.UseAuthorization();   

app.UseHealthChecks("/health");

app.MapControllers();
app.MapProceduresEndpoints();
app.MapEndpoints();
app.MapRazorPages();

app.MapFallbackToPage("/Index");

app.Run();

public partial class Program { }
