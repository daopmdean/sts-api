using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Service.Helpers;
using Service.Interfaces;

namespace Service.Implementations
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _config;

        public EmailSender(EmailConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(Message message)
        {
            var mineMessage = CreateMineMessage(message);

            await SendAsync(mineMessage);
        }

        private MimeMessage CreateMineMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_config.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Content
            };

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mimeMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_config.SmtpServer, _config.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_config.Username, _config.Password);

                await client.SendAsync(mimeMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
