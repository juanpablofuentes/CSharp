using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Bill
{
    public class BillViewModel
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int? ServiceId { get; set; }
        public string Date { get; set; }
        public string Task { get; set; }
        public int InternalIdentifier { get; set; }
        public string Status { get; set; }
        public string ProjectSerie { get; set; }
        public string ExternalSistemNumber { get; set; }
        public string WorkerName { get; set; }
    }
}