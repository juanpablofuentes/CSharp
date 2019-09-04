using System;

namespace Group.Salto.Entities.Tenant
{
    public class ServicesAnalysis
    {
        public int ServiceCode { get; set; }
        public int WorkOrderCode { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public int Technician { get; set; }
        public int? DeliveryNote { get; set; }
        public string Observacions { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndingTime { get; set; }
        public int? OnSiteTime { get; set; }
        public int? TravelTime { get; set; }
        public int? WaitTime { get; set; }
        public decimal? Kilometers { get; set; }
        public string ServiceDescription { get; set; }
        public int? SubcontractorCode { get; set; }
        public string SubcontractorName { get; set; }
        public decimal? StandardPersonCost { get; set; }
        public decimal? KmCost { get; set; }
        public decimal? TravelTimeCost { get; set; }
        public decimal? ProductionCost { get; set; }
        public decimal? SubcontractorCost { get; set; }
        public string ClosingCodeName1 { get; set; }
        public string ClosingCodeDesc1 { get; set; }
        public string ClosingCodeName2 { get; set; }
        public string ClosingCodeDesc2 { get; set; }
        public string ClosingCodeName3 { get; set; }
        public string ClosingCodeDesc3 { get; set; }
        public string ClosingCodeName4 { get; set; }
        public string ClosingCodeDesc4 { get; set; }
        public string ClosingCodeName5 { get; set; }
        public string ClosingCodeDesc5 { get; set; }
        public string ClosingCodeName6 { get; set; }
        public string ClosingCodeDesc6 { get; set; }
        public int? WorkedTime { get; set; }
        
        public WorkOrders WorkOrderCodeNavigation { get; set; }
        public Services Service { get; set; }
    }
}
