using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.Permissions
{
    public class PermissionsListViewModel
    {
        public MultiItemViewModel<PermissionListViewModel, int> Permissions { get; set; }

        public PermissionsFilterViewModel PermissionsFilter { get; set; }
    }
}
