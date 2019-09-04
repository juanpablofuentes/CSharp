using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class PreconditionsLiteralValues : BaseEntity
    {
        public int? LiteralPreconditionId { get; set; }
        public int? QueueId { get; set; }
        public int? WorkOrderStatusId { get; set; }
        public int? ExternalWorOrderStatusId { get; set; }
        public int? PeopleTechnicianId { get; set; }
        public int? PeopleManipulatorId { get; set; }
        public int? OriginId { get; set; }
        public int? WorkOrderTypesN1id { get; set; }
        public int? WorkOrderTypesN2id { get; set; }
        public int? WorkOrderTypesN3id { get; set; }
        public int? WorkOrderTypesN4id { get; set; }
        public int? WorkOrderTypesN5id { get; set; }
        public int? FinalClientId { get; set; }
        public int? LocationId { get; set; }
        public int? AssetId { get; set; }
        public int? EnterValue { get; set; }
        public double? DecimalValue { get; set; }
        public string StringValue { get; set; }
        public bool? BooleanValue { get; set; }
        public DateTime? DataValue { get; set; }
        public int? ProjectId { get; set; }
        public int? WorkOrderCategoryId { get; set; }
        public int? PeopleResponsibleTechniciansCollectionId { get; set; }
        public int? RegionId { get; set; }
        public int? ZoneId { get; set; }

        public ExternalWorOrderStatuses ExternalWorOrderStatus { get; set; }
        public FinalClients FinalClient { get; set; }
        public LiteralsPreconditions LiteralPrecondition { get; set; }
        public Locations Location { get; set; }
        public People PeopleManipulator { get; set; }
        public PeopleCollections PeopleResponsibleTechniciansCollection { get; set; }
        public People PeopleTechnician { get; set; }
        public Projects Project { get; set; }
        public Queues Queue { get; set; }
        public Assets Asset { get; set; }
        public WorkOrderCategories WorkOrderCategory { get; set; }
        public WorkOrderStatuses WorkOrderStatus { get; set; }
        public WorkOrderTypes WorkOrderTypesN1 { get; set; }
        public WorkOrderTypes WorkOrderTypesN2 { get; set; }
        public WorkOrderTypes WorkOrderTypesN3 { get; set; }
        public WorkOrderTypes WorkOrderTypesN4 { get; set; }
        public WorkOrderTypes WorkOrderTypesN5 { get; set; }
        public Zones Zone { get; set; }
    }
}
