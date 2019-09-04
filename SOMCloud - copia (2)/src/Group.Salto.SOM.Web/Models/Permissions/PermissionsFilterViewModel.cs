using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Permissions
{
    public class PermissionsFilterViewModel : BaseFilter
    {
        public PermissionsFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
    }
}
