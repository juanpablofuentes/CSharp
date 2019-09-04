namespace Group.Salto.Entities.Tenant
{
    public class PushNotificationsPeople
    {
        public int NotificationId { get; set; }
        public int PeopleId { get; set; }
        public bool? NotificationToGroup { get; set; }
        public string Status { get; set; }

        public PushNotifications PushNotification { get; set; }
        public People People { get; set; }
    }
}
