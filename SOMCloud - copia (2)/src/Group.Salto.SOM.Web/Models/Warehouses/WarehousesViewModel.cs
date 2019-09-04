using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Warehouses
{
    public class WarehousesViewModel
    {
        public MultiItemViewModel<WarehouseGeneralViewModel, int> Warehouses { get; set; }

        public WarehousesFilterViewModel Filters { get; set; }
    }
}