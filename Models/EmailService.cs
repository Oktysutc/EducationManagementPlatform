using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EducationManagementPlatform.Models
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public void SendEmail(EmailMessage message)
        {
            using (var smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
            {
                smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.Username, _smtpSettings.SenderName),
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(message.To);

                smtpClient.Send(mailMessage);
            }
        }
    }
}
