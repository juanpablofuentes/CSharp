using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WorkOrderType
{
    public class WorkOrderTypeDetailViewModel : WorkOrderTypeViewModel
    {
        public IList<WorkOrderTypeDetailViewModel> Childs { get; set; }
    }
}