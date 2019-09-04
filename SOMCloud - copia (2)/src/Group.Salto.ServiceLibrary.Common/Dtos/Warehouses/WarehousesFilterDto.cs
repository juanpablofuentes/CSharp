namespace Group.Salto.ServiceLibrary.Common.Dtos.Warehouses
{
    public class WarehousesFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string ErpReference { get; set; }
    }
}