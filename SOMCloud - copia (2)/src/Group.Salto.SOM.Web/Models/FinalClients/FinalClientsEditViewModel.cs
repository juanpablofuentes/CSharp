using Group.Salto.Common.Enums;
using Group.Salto.SOM.Web.Models.Calendar;
using Group.Salto.SOM.Web.Models.Contracts;
using Group.Salto.SOM.Web.Models.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.FinalClients
{
    public class FinalClientsEditViewModel
    {
        public ModeActionTypeEnum ModeActionType { get; set; }
        public FinalClientsDetailViewModel FinalClientsDetailViewModel { get; set; }
        public SchedulerViewModel SchedulerViewModel { get; set; }
    }
}