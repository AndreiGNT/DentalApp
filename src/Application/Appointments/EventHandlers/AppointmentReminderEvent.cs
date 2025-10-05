using MediatR;

namespace DentalApp.Application.Appointments.EventHandlers;

public record AppointmentReminderEvent(int AppointmentId, DateTime AppointmentTime) : INotification;

public class AppointmentReminderEventHandler : INotificationHandler<AppointmentReminderEvent>
{
    public Task Handle(AppointmentReminderEvent notification, CancellationToken cancellationToken)
    {
        // the implementation of sending email
        Console.WriteLine($"Reminder: Appointment {notification.AppointmentId} at {notification.AppointmentTime}");
        return Task.CompletedTask;
    }
}
