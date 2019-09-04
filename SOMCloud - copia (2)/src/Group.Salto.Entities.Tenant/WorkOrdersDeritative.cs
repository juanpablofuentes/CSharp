using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class WorkOrdersDeritative : BaseEntity
    {
        public int TaskId { get; set; }
        public string InternalIdentifier { get; set; }
        public string ExternalIdentifier { get; set; }
        public DateTime? CreationDate { get; set; }
        public string TextRepair { get; set; }
        public string Observations { get; set; }
        public DateTime? PickUpTime { get; set; }
        public DateTime? FinalClientClosingTime { get; set; }
        public DateTime? InternalClosingTime { get; set; }
        public DateTime? AssignmentTime { get; set; }
        public DateTime? ActionDate { get; set; }
        public int? PeopleResponsibleId { get; set; }
        public int? OriginId { get; set; }
        public int? PeopleIntroducedById { get; set; }
        public int? WorkOrderTypeId { get; set; }
        public int? LocationId { get; set; }
        public int? ProjectId { get; set; }
        public int? FinalClientId { get; set; }
        public int? WorkOrderStatusId { get; set; }
        public int? ExternalWorOrderStatusId { get; set; }
        public int? IcgId { get; set; }
        public int? AssetId { get; set; }
        public int? PeopleManipulatorId { get; set; }
        public int? QueueId { get; set; }
        public bool? InheritProject { get; set; }
        public bool? InheritTechnician { get; set; }
        public int GeneratorServiceDuplicationPolicy { get; set; }
        public int OtherServicesDuplicationPolicy { get; set; }
        public int? WorkOrderCategoryId { get; set; }
        public int? PeopleResponsibleTechniciansCollectionId { get; set; }
        public int? SiteUserId { get; set; }

        public ExternalWorOrderStatuses ExternalWorOrderStatus { get; set; }
        public FinalClients FinalClient { get; set; }
        public Locations Location { get; set; }
        public People PeopleIntroducedBy { get; set; }
        public People PeopleManipulator { get; set; }
        public People PeopleResponsible { get; set; }
        public PeopleCollections PeopleResponsibleTechniciansCollection { get; set; }
        public Projects Project { get; set; }
        public Queues Queue { get; set; }
        public SiteUser SiteUser { get; set; }
        public Tasks Task { get; set; }
        public Assets Asset { get; set; }
        public WorkOrderCategories WorkOrderCategory { get; set; }
        public WorkOrderStatuses WorkOrderStatus { get; set; }
        public WorkOrderTypes WorkOrderType { get; set; }
        public ICollection<ExtraFieldsValues> ExtraFieldsValues { get; set; }
    }
}
