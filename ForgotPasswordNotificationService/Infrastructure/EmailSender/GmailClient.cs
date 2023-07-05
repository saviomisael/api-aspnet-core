using System.Net;
using System.Net.Mail;
using Infrastructure.Options;

namespace Infrastructure.EmailSender;

public class GmailClient
{
    private readonly GmailOptions _options;
    
    public GmailClient(GmailOptions options)
    {
        _options = options;
    }

    public async Task SendEmail(MailMessage mail)
    {
        using var smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.Credentials = new NetworkCredential(_options.SenderEmail, _options.SenderPassword);
        smtp.EnableSsl = true;
        await smtp.SendMailAsync(mail);
    }
}