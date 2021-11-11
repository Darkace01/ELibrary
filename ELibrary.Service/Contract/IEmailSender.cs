namespace ELibrary.Service.Contract
{
    public interface IEmailSender
    {
        Task Execute(string apiKey, string subject, string message, string email, string fromEmail, string displayName);
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
