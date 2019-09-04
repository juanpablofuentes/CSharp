using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.RolTenant
{
    public class RolTenantFilterViewModel : BaseFilter
    {
        public RolTenantFilterViewModel()
        {
            OrderBy = nameof(Name);
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}