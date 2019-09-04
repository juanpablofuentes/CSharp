using Group.Salto.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.People
{
    public class PeopleFilterViewModel : BaseFilter
    {
        public PeopleFilterViewModel()
        {
            OrderBy = nameof(Name);
        }

        public string Name { get; set; }
        public ActiveEnum Active { get; set; }
    }
}