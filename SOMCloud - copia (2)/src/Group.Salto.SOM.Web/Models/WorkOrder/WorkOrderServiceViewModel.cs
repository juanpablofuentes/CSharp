using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderServiceViewModel
    {
        public WorkOrderServiceViewModel()
        {
            WOService = new List<WorkOrderFormsViewModel>();
        }
        public IList<WorkOrderFormsViewModel> WOService { get; set; }
    }
}