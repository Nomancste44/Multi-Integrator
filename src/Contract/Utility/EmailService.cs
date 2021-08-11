using MimeKit;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Models;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ZohoToInsightIntegrator.Contract.Utility
{
    public class EmailService
    {
        private readonly EmailConfigurationOptions _options;

        public EmailService(EmailConfigurationOptions options)
        {
            _options = options;
        }

        public async Task SendPasswordAsync(string email, string password)
        {
            var body = string.Empty;
            body += $"<p>Dear {email},</p><br /><br />";
            body += $"<p>Your new password is : <b>{password}</b> </p><br />";
            body += "<p>Please log into portal and change this password as soon as possible.</p><br />";
            body += $"<p>If you did not request a new password, please forward this email to the {_options.HelpLineEmail}</p>";

            var emailMessage = CreateMimeMessage(email);
            emailMessage.Subject = "Your new password for Zoho CRM To Insight Integrator";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateMimeMessage(string mailTo)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_options.From));
            emailMessage.To.Add(MailboxAddress.Parse(mailTo));
            return emailMessage;
        }
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_options.SmtpServer, _options.Port, true);
                await client.AuthenticateAsync(_options.UserName, _options.Password);
                await client.SendAsync(mailMessage);
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
