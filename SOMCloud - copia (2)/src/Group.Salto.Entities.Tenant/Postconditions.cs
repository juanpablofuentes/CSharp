using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class Postconditions : BaseEntity
    {
        public int PostconditionCollectionsId { get; set; }
        public string NameFieldModel { get; set; }
        public int? QueueId { get; set; }
        public int? WorkOrderStatusId { get; set; }
        public int? ExternalWorOrderStatusId { get; set; }
        public int? PeopleTechniciansId { get; set; }
        public int? PeopleManipulatorId { get; set; }
        public int? EnterValue { get; set; }
        public double? DecimalValue { get; set; }
        public string StringValue { get; set; }
        public bool? BooleanValue { get; set; }
        public DateTime? DateValue { get; set; }
        public int? ExtraFieldId { get; set; }
        public int? PeopleResponsibleTechniciansCollectionId { get; set; }
        public Guid PostconditionsTypeId { get; set; }

        public ExternalWorOrderStatuses ExternalWorOrderStatus { get; set; }
        public ExtraFields ExtraField { get; set; }
        public People PeopleManipulator { get; set; }
        public PeopleCollections PeopleResponsibleTechniciansCollection { get; set; }
        public People PeopleTechnicians { get; set; }
        public PostconditionCollections PostconditionCollections { get; set; }
        public Queues Queue { get; set; }
        public WorkOrderStatuses WorkOrderStatus { get; set; }
    }
}
