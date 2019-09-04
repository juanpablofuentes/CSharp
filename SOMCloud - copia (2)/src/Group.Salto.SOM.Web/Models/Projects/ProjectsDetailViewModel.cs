using Group.Salto.Common.Enums;
using Group.Salto.SOM.Web.Models.PredefinedServices;
using Group.Salto.SOM.Web.Models.Scheduler;
using Group.Salto.SOM.Web.Models.Technicians;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Projects
{
    public class ProjectsDetailViewModel
    {
        public ProjectsDetailViewModel()
        {
            GenericDetailViewModel = new GenericDetailViewModel();
            TechniciansSelected = new List<TechniciansEditViewModel>();
            PredefinedServicesSelected = new List<PredefinedServicesEditViewModel>();
        }

        public ModeActionTypeEnum ModeActionType { get; set; }
        public GenericDetailViewModel GenericDetailViewModel { get; set; }
        public SchedulerViewModel SchedulerViewModel { get; set; }
        public IList<TechniciansEditViewModel> TechniciansSelected { get; set; }
        public IList<PredefinedServicesEditViewModel> PredefinedServicesSelected { get; set; }
    }
}