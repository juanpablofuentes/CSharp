using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Bill
{
    public class BillFilterDto : BaseFilterDto
    {
        public int? WorkOrderId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InternalIdentifier { get; set; }
        public string ProjectSerie { get; set; }
        public string Project { get; set; }
        public string Status { get; set; }
        public int? ItemId { get; set; }
        public int Count { get; set; }
        public int Id { get; set; }
    }
}