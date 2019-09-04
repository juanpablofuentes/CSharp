namespace Group.Salto.ServiceLibrary.Common.Contracts.Notification
{
    public interface INotificationService
    {
        void SendNotification(INotificationRequest notificationRequest);
        void SendNotificationToMultipleRecipients(INotificationRequest notificationRequest);
    }
}