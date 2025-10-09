using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Service;
using DentalApp.Domain.Entities;
using DentalApp.Infrastructure;
using DentalApp.Infrastructure.Data;
using DentalApp.Web;
using DentalApp.Web.Endpoints;
using DentalApp.Web.Endpoints.Appointments;
using DentalApp.Web.Endpoints.Procedures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddKeyVaultIfConfigured();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
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
//builder.Services.AddHostedService<AppointmentReminderService>();
builder.Services.AddTransient<IEmailSender, EmailSenderService>();

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
