using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace DentalApp.Application.Common.Service;

public class EmailSenderService : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSenderService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Recipient email is null or empty.", nameof(email));
        }

        var emailMessage = new MimeMessage
        {
            Subject = subject,
            Body = new TextPart("html") { Text = htmlMessage }
        };

        emailMessage.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
        emailMessage.To.Add(MailboxAddress.Parse(email));

        var smtpServer = _config["EmailSettings:SmtpServer"];
        var username = _config["EmailSettings:Username"];
        var password = _config["EmailSettings:Password"];

        if (!int.TryParse(_config["EmailSettings:Port"], out int port))
        {
            throw new InvalidOperationException("EmailSettings:Port is missing or not a valid number.");
        }

        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(smtpServer, port, true);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(emailMessage);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to send email.", ex);
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}
