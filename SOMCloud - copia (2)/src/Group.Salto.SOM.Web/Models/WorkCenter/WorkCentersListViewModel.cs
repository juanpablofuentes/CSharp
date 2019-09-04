using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.WorkCenter
{
    public class WorkCentersListViewModel
    {
        public MultiItemViewModel<WorkCenterListViewModel, int> WorkCenters { get; set; }
        public WorkCenterFilterViewModel WorkCenterFilters { get; set; }
    }
}