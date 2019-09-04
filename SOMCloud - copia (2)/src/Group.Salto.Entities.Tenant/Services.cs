using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Services : BaseEntity
    {
        public int PredefinedServiceId { get; set; }
        public string IdentifyInternal { get; set; }
        public string IdentifyExternal { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int? PeopleResponsibleId { get; set; }
        public int? SubcontractResponsibleId { get; set; }
        public int WorkOrderId { get; set; }
        public int? ServiceStateId { get; set; }
        public int? ClosingCodeFirstId { get; set; }
        public int? ClosingCodeSecondId { get; set; }
        public int? ClosingCodeThirdId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? IcgId { get; set; }
        public DateTime CreationDate { get; set; }
        public string FormState { get; set; }
        public string DeliveryNote { get; set; }
        public DateTime? DeliveryProcessInit { get; set; }
        public bool? Cancelled { get; set; }
        public int? ServicesCancelFormId { get; set; }
        public int? ClosingCodeId { get; set; }

        public ServicesAnalysis ServicesAnalysis { get; set; }
        public ClosingCodes ClosingCode { get; set; }
        public ClosingCodes ClosingCodeFirst { get; set; }
        public ClosingCodes ClosingCodeSecond { get; set; }
        public ClosingCodes ClosingCodeThird { get; set; }
        public People PeopleResponsible { get; set; }
        public PredefinedServices PredefinedService { get; set; }
        public Services ServicesCancelForm { get; set; }
        public SubContracts SubcontractResponsible { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public ICollection<ExtraFieldsValues> ExtraFieldsValues { get; set; }
        public ICollection<Services> InverseServicesCancelForm { get; set; }
        public WorkOrders WorkOrder { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WarehouseMovements> WarehouseMovements { get; set; }
    }
}
