using ELibrary.Service.Contract;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ELibrary.Service.Implementation;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;
    public EmailSender(IConfiguration config)
    {
        _config = config;
    }
    public Task SendEmailAsync(string toEmail, string subject, string message)
    {
        string apiKey = _config["SendGrid:ApiSecret"];
        string fromEmail = _config["SendGrid:FromEmail"];
        string displayName = _config["SendGrid:DisplayName"];
        return Execute(apiKey, subject, message, toEmail, fromEmail, displayName);
    }

    public Task Execute(string apiKey, string subject, string message, string email, string fromEmail, string displayName)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(fromEmail, displayName),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(email));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);

        return client.SendEmailAsync(msg);
    }
}
