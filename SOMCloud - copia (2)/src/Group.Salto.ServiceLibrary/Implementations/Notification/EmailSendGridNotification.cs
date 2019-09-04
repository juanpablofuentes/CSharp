using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Dtos.Notification;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;

namespace Group.Salto.ServiceLibrary.Implementations.Notification
{
    public class EmailSendGridNotification : IEmailSendGridNotification
    {
        private INotificationConfigurationService _emailConfiguration;

        public EmailSendGridNotification(INotificationConfigurationService emailConfiguration)
        {
            _emailConfiguration = emailConfiguration ?? throw new ArgumentNullException($"{nameof(emailConfiguration)} is null ");
        }

        public void SendNotification(INotificationRequest notificationRequest)
        {
            var emailMessage = ((EmailMessageDto)notificationRequest);
            var config = _emailConfiguration.GetSendGridConfiguration(emailMessage.ConnectionString);
            var client = new SendGridClient(config.SendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(config.EmailFrom, config.EmailFrom),
                Subject = emailMessage.Subject,
                PlainTextContent = emailMessage.Body,
                HtmlContent = emailMessage.Body
            };
            msg.AddTo(new EmailAddress(emailMessage.To));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            client.SendEmailAsync(msg);
        }

        public void SendNotificationToMultipleRecipients(INotificationRequest notificationRequest)
        {
            var emailMessage = ((EmailMessageDto)notificationRequest);
            var config = _emailConfiguration.GetSendGridConfiguration(emailMessage.ConnectionString);
            var client = new SendGridClient(config.SendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(config.EmailFrom, config.EmailFrom),
                Subject = emailMessage.Subject,
                PlainTextContent = emailMessage.Body,
                HtmlContent = emailMessage.Body
            };
            foreach (var recipient in emailMessage.Recipients)
            {
                msg.AddTo(new EmailAddress(recipient));
            }

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            client.SendEmailAsync(msg);
        }
    }
}