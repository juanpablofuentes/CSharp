using Group.Salto.Common.Enums;
using Group.Salto.SOM.Web.Models.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.People
{
    public class PeopleEditViewModel
    {
        public ModeActionTypeEnum ModeActionType { get; set; }
        public PersonalEditViewModel PersonalEditViewModel { get; set; }
        public AccessEditViewModel AccessEditViewModel { get; set; }
        public WorkEditViewModel WorkEditViewModel { get; set; }
        public GeoLocalitzationEditViewModel GeoLocalitzationEditViewModel { get; set; }
        public IList<PeopleCostEditViewModel> CostSelected { get; set; }
        public SchedulerViewModel SchedulerViewModel { get; set; }
        public string FormatDate { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern.ToUpper();
    }
}