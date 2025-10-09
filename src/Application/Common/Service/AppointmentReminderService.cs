using DentalApp.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DentalApp.Application.Common.Service;
public class AppointmentReminderService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public AppointmentReminderService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                var tomorrow = DateTime.Now.Date.AddDays(1);
                var dayStart = tomorrow;
                var dayEnd = tomorrow.AddDays(1).AddSeconds(-1);

                var appointments = await dbContext.Appointments
                    .Where(a => a.StartTime >= dayStart && a.StartTime <= dayEnd)
                    .Include(a => a.User)
                    .Include(a => a.Doctor)
                    .Include(a => a.Procedure)
                    .ToListAsync();

                foreach (var appt in appointments)
                {
                    var userEmail = appt.User.Email;
                    var doctorName = appt.Doctor.FullName;
                    var procedure = appt.Procedure.ProcedureName;
                    var time = appt.StartTime.ToString("f");

                    var message = $"Hello {appt.User.FirstName},<br><br>"
                                + $"You have an appointment with {doctorName} for the procedure <strong>{procedure}</strong> "
                                + $"scheduled on <strong>{time}</strong>.<br><br>"
                                + "Please confirm your attendance or let us know if you won’t be able to come.";

                    if (!string.IsNullOrWhiteSpace(userEmail))
                    {
                        await emailSender.SendEmailAsync(userEmail, "Appointment Reminder", message);
                    }
                }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}
