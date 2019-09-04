using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WarehouseMovements
{
    public class WarehouseMovementsViewModel
    {
        public WarehouseMovementsFiltersViewModel Filters { get; set; }
        public MultiItemViewModel<WarehouseMovementsLineViewModel, int> Movements { get; set; }
    }
}