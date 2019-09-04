using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.Vehicles
{
    public class VehiclesViewModel
    {
        public MultiItemViewModel<VehicleViewModel, int> Vehicle { get; set; }

        public VehiclesFilterViewModel VehicleFilters { get; set; }
    }
}