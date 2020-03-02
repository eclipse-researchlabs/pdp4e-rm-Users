using System.Net.Mail;
using MailKit.Net.Smtp;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Core.Users.Implementation.Helpers
{
    public interface IEmailHelper
    {
        bool Send(string toEmail, string toName, string subject, string body);
    }

    public class EmailHelper : IEmailHelper
    {
        public static MailboxConfig Config { get; set; }
        public bool Send(string toEmail, string toName, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Config.DisplayName, Config.Username));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(Config.Url, 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(Config.Username, Config.Password);

                client.Send(message);
                client.Disconnect(true);
                return true;
            }
        }

        public class MailboxConfig
        {
            public string DisplayName { get; set; }
            public string Url { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
