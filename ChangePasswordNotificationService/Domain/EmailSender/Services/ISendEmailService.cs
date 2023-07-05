namespace Domain.EmailSender.Services;

public interface ISendEmailService
{
    Task SendEmail(string email, string username);
}