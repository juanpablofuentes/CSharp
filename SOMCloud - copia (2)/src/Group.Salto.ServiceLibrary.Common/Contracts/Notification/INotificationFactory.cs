using Group.Salto.Common.Enums;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Notification
{
    public interface INotificationFactory
    {
        INotificationService GetService(NotificationTypeEnum typeNotification);
    }
}