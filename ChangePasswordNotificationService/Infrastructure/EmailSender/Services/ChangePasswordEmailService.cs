using System.Net.Mail;
using Domain.EmailSender.Services;
using Infrastructure.Options;

namespace Infrastructure.EmailSender.Services;

public class ChangePasswordEmailService : ISendEmailService
{
    private readonly GmailClient _client;
    private readonly GmailOptions _options;

    public ChangePasswordEmailService(GmailClient client, GmailOptions options)
    {
        _client = client;
        _options = options;
    }

    public async Task SendEmail(string email, string username)
    {
        var datetimeInUnix = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        using var mail = new MailMessage();
        mail.From = new MailAddress(_options.SenderEmail);
        mail.To.Add(email);
        mail.Subject = $"Your password was changed in {DateTime.UtcNow}";
        mail.IsBodyHtml = true;
        mail.Body = $@"
            <span style=""visibility: hidden; color: transparent"">{datetimeInUnix}</span>
            <div
                style=""
                background: linear-gradient(0deg, rgba(34, 193, 195, 1) 0%, rgba(253, 187, 45, 1) 100%);
                width: 90vw;
                margin: 0 auto;
                height: 90vh;
                display: flex;
                align-items: center;
                justify-content: center;
                ""
            >
            <p style=""font-family: sans-serif; color: white; font-size: 45px; text-align: center"">
                     Hello {username} your password was changed successfully.
            </p>
            </div>
        ";

        await _client.SendEmail(mail);
    }
}