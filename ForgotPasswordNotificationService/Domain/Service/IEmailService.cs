using Domain.Entity;

namespace Domain.Service;

public interface IEmailService
{
    Task SendEmail(EmailReceiver receiver);
}