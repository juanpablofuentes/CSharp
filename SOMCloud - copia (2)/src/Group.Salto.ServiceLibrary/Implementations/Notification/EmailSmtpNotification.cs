using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Dtos.Notification;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Group.Salto.ServiceLibrary.Implementations.Notification
{
    public class EmailSmtpNotification : IEmailSmtpNotification
    {
        private readonly ILoggingService _loggingService;
        private readonly INotificationConfigurationService _emailConfiguration;
        private EmailSmtpConfigurationDto _emailSmtpConfiguration;

        public EmailSmtpNotification(ILoggingService loggingService, INotificationConfigurationService emailConfiguration)
        {
            _emailConfiguration = emailConfiguration ?? throw new ArgumentNullException($"{nameof(emailConfiguration)} is null ");
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(loggingService)} is null ");
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate,
                    X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
        }

        public void SendNotification(INotificationRequest notificationRequest)
        {
            var emailMessage = ((EmailMessageDto)notificationRequest);
            _emailSmtpConfiguration = _emailConfiguration.GetSmtpConfiguration(emailMessage.ConnectionString);
            var client = GetClient();
            var mailMessage = new MailMessage(_emailSmtpConfiguration.EmailFrom, emailMessage.To)
            {
                From = new MailAddress(_emailSmtpConfiguration.EmailFrom),
                Subject = emailMessage.Subject,
                Body = emailMessage.Body,
                IsBodyHtml = true,
            };

            _loggingService.LogInfo($"Sending email to {emailMessage.To}");
            client.Send(mailMessage);
        }

        public void SendNotificationToMultipleRecipients(INotificationRequest notificationRequest)
        {
            var emailMessage = ((EmailMessageDto)notificationRequest);
            _emailSmtpConfiguration = _emailConfiguration.GetSmtpConfiguration(emailMessage.ConnectionString);
            var client = GetClient();
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSmtpConfiguration.EmailFrom),
                Subject = emailMessage.Subject,
                Body = emailMessage.Body,
                IsBodyHtml = true,
            };

            foreach (var mail in emailMessage.Recipients)
            {
                mailMessage.To.Add(mail);
            }

            _loggingService.LogInfo($"Sending email to {emailMessage.To}");
            client.Send(mailMessage);
        }

        private SmtpClient GetClient()
        {
            var client = new SmtpClient(_emailSmtpConfiguration.SmtpServer)
            {
                EnableSsl = _emailSmtpConfiguration.SmtpEnableSSL,
                Port = _emailSmtpConfiguration.SmtpPort,
                Credentials = GetNetworkCredentials(_emailSmtpConfiguration.SmtpUserName, _emailSmtpConfiguration.SmtpPassword,"")
            };
            return client;
        }

        private ICredentialsByHost GetNetworkCredentials(string username, string password, string domain)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var result = !string.IsNullOrEmpty(domain)
                    ? new NetworkCredential(username, password)
                    : new NetworkCredential(username, password, domain);
                return result;
            }
            else
            {
                _loggingService.LogError("Email configuration invalid");
                throw new ArgumentNullException("Email configuration invalid");
            }
        }
    }
}