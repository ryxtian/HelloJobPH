namespace HelloJobPH.Server.Service.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);

    }
}
