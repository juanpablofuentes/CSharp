using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.People
{
    public class PeoplesViewModel
    {
        public MultiItemViewModel<PeopleViewModel, int> Peoples { get; set; }
        public PeopleFilterViewModel PeopleFilters { get; set; }
    }
}