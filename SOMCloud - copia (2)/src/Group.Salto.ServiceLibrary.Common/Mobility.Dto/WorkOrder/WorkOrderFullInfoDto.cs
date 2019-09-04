using Group.Salto.ServiceLibrary.Common.Dtos.Location;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.Queue;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrderType;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrder
{
    public class WorkOrderFullInfoDto
    {
        public int Id { get; set; }
        public string InternalIdentifier { get; set; }
        public string ExternalIdentifier { get; set; }
        public DateTime CreationDate { get; set; }
        public string TextRepair { get; set; }
        public string Observations { get; set; }
        public DateTime? PickUpTime { get; set; }
        public DateTime? FinalClientClosingTime { get; set; }
        public DateTime? InternalClosingTime { get; set; }
        public DateTime? AssignmentTime { get; set; }
        public DateTime? ActionDate { get; set; }
        public DateTime? DateStopTimerSla { get; set; }
        public DateTime? ResponseDateSla { get; set; }
        public DateTime? ResolutionDateSla { get; set; }
        public DateTime? DateUnansweredPenaltySla { get; set; }
        public DateTime? DatePenaltyWithoutResolutionSla { get; set; }
        public bool ReferenceGeneratorService { get; set; }
        public bool ReferenceOtherServices { get; set; }
        public bool Archived { get; set; }
        public DateTime? ActuationEndDate { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsResponsibleFixed { get; set; }
        public bool IsActuationDateFixed { get; set; }
        public bool Billable { get; set; }
        public DateTime? ClosingOtdate { get; set; }
        public DateTime? AccountingClosingDate { get; set; }
        public DateTime? ClientClosingDate { get; set; }
        public DateTime? SystemDateWhenOtclosed { get; set; }
        public DateTime? InternalCreationDate { get; set; }
        public string ClientSiteName { get; set; }
        public WorkOrderCategoriesListDto WorkOrderCategory { get; set; }
        public QueueBaseDto Queue { get; set; }
        public WorkOrderStatusBaseDto WorkOrderStatus { get; set; }
        public WorkOrderStatusBaseDto ExternalWorkOrderStatus { get; set; }
        public PeopleDto PeopleResponsible { get; set; }
        public LocationDto ClientSite { get; set; }
        public AssetDto Equipment { get; set; }
        public IEnumerable<WorkOrderFatherListDto> WorkOrderTypes { get; set; }
        public IEnumerable<WoServiceDto> WoServices { get; set; }
        public string BackOfficeResponsiblePhone { get; set; }
        public string TechnicalResponsiblePhone { get; set; }
    }
}