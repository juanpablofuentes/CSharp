namespace Group.Salto.Entities.Tenant
{
    public class PushNotificationsPeopleCollections
    {
        public int NotificationId { get; set; }
        public int PeopleCollectionsId { get; set; }

        public PushNotifications Notification { get; set; }
        public PeopleCollections PeopleCollections { get; set; }
    }
}
