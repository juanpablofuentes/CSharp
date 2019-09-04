using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Common.Dtos.Notification;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Notification
{
    public interface INotificationConfigurationService
    {
        NotificationTypeEnum GetNotificationTypeConfiguration(string connectionString);
        EmailSendGridConfigurationDto GetSendGridConfiguration(string connectionString);
        EmailSmtpConfigurationDto GetSmtpConfiguration(string connectionString);
    }
}