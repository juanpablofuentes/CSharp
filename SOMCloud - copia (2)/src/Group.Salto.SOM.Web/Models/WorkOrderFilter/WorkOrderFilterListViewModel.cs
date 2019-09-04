using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.WorkOrderFilter
{
    public class WorkOrderFilterListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Load { get; set; }

        public string GetViewListId()
        {
            return "ViewList" + Id;
        }
    }
}