
using System.Net.Mail;
using assignement_3.DAL.Models;
using MailKit.Net.Smtp;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using MimeKit;

namespace assignement_3.PL.Helpers
{
    public class MailServices : IMailServices
    {
        private readonly IOptions<EmailSettings> _options;

        public MailServices(IOptions<EmailSettings> options)
        {
            _options = options;
        }
        public void sendEmail(Email email)
        {
            var mail = new MimeMessage();

            mail.Subject = email.subject;
            mail.From.Add(new MailboxAddress(_options.Value.DisplayName,_options.Value.Email));
            mail.To.Add(MailboxAddress.Parse(email.To));

            var builder = new BodyBuilder();
            builder.TextBody = email.body;
            mail.Body = builder.ToMessageBody();

            // Establish connection

            using var smpt = new MailKit.Net.Smtp.SmtpClient();
            smpt.Connect(_options.Value.Host, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
            smpt.Authenticate(_options.Value.Email, _options.Value.Password);

            smpt.Send(mail);
        }
    }
}
