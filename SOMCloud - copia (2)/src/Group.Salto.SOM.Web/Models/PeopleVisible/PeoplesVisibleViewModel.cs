using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.PeopleVisible
{
    public class PeoplesVisibleViewModel
    {
        public MultiItemViewModel<PeopleVisibleViewModel, int> PeoplesVisibles { get; set; }
        public PeopleVisibleFilterViewModel PeopleVisibleFilters { get; set; }
    }
}