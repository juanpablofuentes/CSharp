using System;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderDetailViewModel
    {
        public WorkOrderDetailViewModel()
        {
            GenericDetailViewModel = new GenericDetailViewModel();
            WorkOrderFormsViewModel = new WorkOrderFormsViewModel();
        }

        public GenericDetailViewModel GenericDetailViewModel { get; set; }
        public WorkOrderFormsViewModel WorkOrderFormsViewModel { get; set; }
    }
}