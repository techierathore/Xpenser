using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace Xpenser.API.Common
{
    public interface IEmailService
    {
        public void SendEmail(string aRecipientName, string aRecipientAddress, string aSubject, string aText);
    }
    public class EmailService
    {
        private readonly EmailSettings Options;

        public EmailService(AppSettings aSettings)
        {
            Options = aSettings.EmailOptions;
        }

        public void SendEmail(string aRecipientName, string aRecipientAddress, string aSubject, string aText)
        {
            var vSender = new MailboxAddress(Options.SenderName, Options.SenderAddress);
            var vRecipient = new MailboxAddress(aRecipientName, aRecipientAddress);

            var vMessage = new MimeMessage
            {
                Subject = aSubject,
                Body = new TextPart("html")
                {
                    Text = aText
                }
            };

            vMessage.From.Add(vSender);
            vMessage.To.Add(vRecipient);
            var copyAddress = new MailboxAddress(string.Empty, Options.CopyAddress);
            vMessage.Cc.Add(copyAddress);

            Task.Run(() =>
            {
                using var vClient = new SmtpClient();
                vClient.Connect(Options.Host, Options.Port, Options.UseSsl);
                // uncomment if the SMTP server requires authentication
                vClient.Authenticate(Options.UserName, Options.Password);
                vClient.Send(vMessage);
                vClient.Disconnect(true);
            });
        }
    }
}
