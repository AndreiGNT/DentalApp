using Microsoft.Extensions.Logging;

namespace DentalApp.Application.Appointments.EventHandlers;

public class AppointmentCreatedEventHandler
{
    private readonly ILogger<AppointmentCreatedEventHandler>? _logger;

    public AppointmentCreatedEventHandler(ILogger<AppointmentCreatedEventHandler>? logger)
    {
        _logger = logger;
    }
}
