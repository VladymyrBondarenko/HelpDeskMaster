using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace HelpDeskMaster.Infrastracture.Mailing
{
    internal class EmailService : IEmailService
    {
        private readonly EmailSenderOptions _emailSenderOptions;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            EmailSenderOptions emailSettings,
            ILogger<EmailService> logger)
        {
            _emailSenderOptions = emailSettings;
            _logger = logger;
        }

        public async Task<bool> SendAsync(EmailSendParams sendParams, CancellationToken cancellationToken)
        {
            try
            {
                using var emailMessage = new MimeMessage();

                var emailFrom = new MailboxAddress(_emailSenderOptions.SenderName, _emailSenderOptions.SenderEmail);
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(sendParams.EmailToName, sendParams.EmailToId);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = sendParams.EmailSubject;

                var emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = sendParams.EmailBody;

                emailMessage.Body = emailBodyBuilder.ToMessageBody();

                using var emailClient = new SmtpClient();

                await emailClient.ConnectAsync(
                    _emailSenderOptions.Server,
                    _emailSenderOptions.Port,
                    MailKit.Security.SecureSocketOptions.StartTls,
                    cancellationToken);

                await emailClient.AuthenticateAsync(
                    _emailSenderOptions.UserName,
                    _emailSenderOptions.Password,
                    cancellationToken);

                await emailClient.SendAsync(emailMessage, cancellationToken);
                await emailClient.DisconnectAsync(true, cancellationToken);
            }
            catch(Exception caughtException)
            {
                _logger.LogError(caughtException, 
                    "Exception while sending email to {email} with content: {content}",
                    sendParams.EmailToId, sendParams.EmailBody);

                throw;
            }

            return true;
        }
    }
}
