using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications
{
    public class ProcessNotificationValues
    {
        public People People { get; set; }
        public WorkOrders WorkOrder { get; set; }
        public NotificationTemplateTypeEnum NotificationType { get; set; }
    }
}
