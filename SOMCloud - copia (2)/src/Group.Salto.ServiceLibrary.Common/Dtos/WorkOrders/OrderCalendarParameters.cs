using Group.Salto.Common.Enums;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class OrderCalendarParameters
    {
        public OrderCalendarParameters(EntitiesWithCalendarsEnum entityType, int entityId, int preference)
        {
            EntityType = entityType;
            EntityId = entityId;
            Preference = preference;
        }

        public EntitiesWithCalendarsEnum EntityType { get; set; }
        public int EntityId { get; set; }
        public int Preference { get; set; }
    }
}