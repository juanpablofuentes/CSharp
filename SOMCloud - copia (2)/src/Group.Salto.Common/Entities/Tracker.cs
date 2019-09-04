using System;

namespace Group.Salto.Common.Entities
{
    public class Tracker : BaseEntity<Guid>
    {
        public Tracker() { }

        public Tracker(string entityType, string propertyName, string timeStamp, string entityId, string ownerId, Guid transactionId)
        {
            EntityType = entityType;
            PropertyName = propertyName;
            TimeStamp = timeStamp;
            EntityId = entityId;
            OwnerId = ownerId;
            TransactionId = transactionId;
        }

        public string EntityType { get; set; }
        public string PropertyName { get; set; }
        public string TimeStamp { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string EntityId { get; set; }
        public string OwnerId { get; set; }
        public Guid TransactionId { get; set; }
    }
}
