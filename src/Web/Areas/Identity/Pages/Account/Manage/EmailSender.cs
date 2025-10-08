using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace DentalApp.Web.Areas.Identity.Pages.Account.Manage;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
        emailMessage.To.Add(MailboxAddress.Parse(email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("html") { Text = htmlMessage };

        using var smtp = new SmtpClient();

        var smtpServer = _config["EmailSettings:SmtpServer"];
        var portString = _config["EmailSettings:Port"];
        var username = _config["EmailSettings:Username"];
        var password = _config["EmailSettings:Password"];

        if (!int.TryParse(portString, out int port))
            throw new InvalidOperationException("EmailSettings:Port is missing or not a valid number.");

        await smtp.ConnectAsync(smtpServer, port, true);
        await smtp.AuthenticateAsync(username, password);
        await smtp.SendAsync(emailMessage);
        await smtp.DisconnectAsync(true);
    }
}
