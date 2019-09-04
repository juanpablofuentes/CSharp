using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Scheduler;
using Group.Salto.SOM.Web.Models.Technicians;
using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WorkOrderCategory
{
    public class WorkOrderCategoryDetailViewModel
    {
        public WorkOrderCategoryDetailViewModel()
        {
            GenericEditViewModel = new GenericEditViewModel();
        }

        public ModeActionTypeEnum ModeActionType { get; set; }
        public GenericEditViewModel GenericEditViewModel { get; set; }
        public MultiSelectViewModel Permissions { get; set; }
        public MultiSelectViewModel Roles { get; set; }
        public IList<TechniciansEditViewModel> TechniciansSelected { get; set; }
        public SchedulerViewModel SchedulerViewModel { get; set; }
        public IList<MultiComboViewModel<int, int>> KnowledgeSelected { get; set; }
    }
}