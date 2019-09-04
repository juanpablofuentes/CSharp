using Group.Salto.Common.Enums;
using Group.Salto.SOM.Web.Models.Contracts;
using Group.Salto.SOM.Web.Models.Scheduler;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Sites
{
    public class SitesDetailViewModel
    {
        public SitesDetailViewModel()
        {
            GenericDetailViewModel = new GenericDetailViewModel();
        }
        public ModeActionTypeEnum ModeActionType { get; set; }
        public GenericDetailViewModel GenericDetailViewModel { get; set; }
        public GeolocationDetailViewModel GeolocationDetailViewModel { get; set; }
        public SchedulerViewModel SchedulerViewModel { get; set; }
    }
}