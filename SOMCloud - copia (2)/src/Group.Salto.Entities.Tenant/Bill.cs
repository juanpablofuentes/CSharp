using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Bill : BaseEntity
    {
        public int WorkorderId { get; set; }
        public int? ServiceId { get; set; }
        public string Task { get; set; }
        public int PeopleId { get; set; }
        public DateTime Date { get; set; }
        public string ExternalSystemNumber { get; set; }
        public int Status { get; set; }
        public int? TaskId { get; set; }
        public int ErpSystemInstanceId { get; set; }

        public ErpSystemInstance ErpSystemInstance { get; set; }
        public People People { get; set; }
        public Services Service { get; set; }
        public Tasks TaskNavigation { get; set; }
        public WorkOrders Workorder { get; set; }
        public ICollection<BillLine> BillLine { get; set; }
        public ICollection<DnAndMaterialsAnalysis> DnAndMaterialsAnalysis { get; set; }
        
    }
}
