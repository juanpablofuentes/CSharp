using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Scheduler
{
    public class SchedulerViewModel
    {
        public int Id { get; set; }
        public IList<BaseNameIdDto<int>> CalendarEventCategory { get; set; }
    }
}