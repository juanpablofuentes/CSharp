namespace Group.Salto.SOM.Web.Models.Warehouses
{
    public class WarehousesFilterViewModel : BaseFilter
    {
        public WarehousesFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string ERPReference { get; set; }
    }
}