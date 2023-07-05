using System.Net.Mail;
using Domain.Entity;
using Domain.Service;
using Infrastructure.Options;

namespace Infrastructure.EmailSender.Service;

public class SendRandomPassswordEmailService : IEmailService
{
    private readonly GmailClient _client;
    private readonly GmailOptions _options;

    public SendRandomPassswordEmailService(GmailClient client, GmailOptions options)
    {
        _client = client;
        _options = options;
    }
    
    public async Task SendEmail(EmailReceiver receiver)
    {
        var datetimeInUnix = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        using var mail = new MailMessage();
        mail.From = new MailAddress(_options.SenderEmail);
        mail.To.Add(receiver.Email);
        mail.Subject = $"Here is your temporary password / {DateTime.UtcNow}";
        mail.IsBodyHtml = true;
        mail.Body = $@"
            <span style=""visibility: hidden; color: transparent"">{datetimeInUnix}</span>
            <div
        style=""
        background: linear-gradient(0deg, rgba(34, 193, 195, 1) 0%, rgba(253, 187, 45, 1) 100%);
        width: 100vw;
        margin: 0 auto;
        height: 100%;
        padding: 10px;
        ""
            >
            <p style=""font-family: sans-serif; color: white; font-size: 45px; text-align: center; margin: 0"">
                     Here is your temporary password: <em>{receiver.RandomPassword}</em>.
            </p>
            <p style=""font-family: sans-serif; color: white; font-size: 45px; text-align: center; margin: 0"">
                     Hello {receiver.UserName}, you request a temporary password and it will 
            <strong>expire in one hour.</strong>
            </p>
            <p style=""font-family: sans-serif; color: white; font-size: 45px; text-align: center; margin: 0"">
                     Access your profile with this temporary password and change your password to access your account again.
            </p>
        </div>
        ";

        await _client.SendEmail(mail);
    }
}