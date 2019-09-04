using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Task
{
    public class TasksListViewModel
    {
        public List<BaseNameIdDto<int>> TasksListDictionary { get; set; }
    }
}