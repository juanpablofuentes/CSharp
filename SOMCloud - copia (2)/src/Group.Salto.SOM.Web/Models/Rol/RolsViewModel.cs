using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.Rol
{
    public class RolsViewModel
    {
        public MultiItemViewModel<RolViewModel, int> Roles { get; set; }
        public RolFilterViewModel RolFilters { get; set; }
    }
}