using System;
using Group.Salto.Common.Enums;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderEditViewModel
    {
        public WorkOrderEditViewModel()
        {
            WorkOrderGenericEditViewModel = new WorkOrderGenericEditViewModel();
        }

        public ModeActionTypeEnum ModeActionType { get; set; }
        public WorkOrderGenericEditViewModel WorkOrderGenericEditViewModel { get; set; }
    }
}