using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.RolTenant
{
    public class RolTenantListViewModel
    {
        public MultiItemViewModel<RolTenantViewModel, int> RolesTenant { get; set; }
        public RolTenantFilterViewModel RolTenantFilters { get; set; }
    }
}