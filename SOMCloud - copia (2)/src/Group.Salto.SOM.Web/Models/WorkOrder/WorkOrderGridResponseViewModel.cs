using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderGridResponseViewModel
    {
        public WorkOrderFilterGridViewModel WorkOrderFilterGridViewModel { get; set; }
        public int PaginationNumber { get; set; }
    }
}