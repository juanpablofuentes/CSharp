using Group.Salto.ServiceLibrary.Common.Dtos.Warehouses;
using Group.Salto.SOM.Web.Models.Warehouses;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WarehousesFilterViewModelExtensions
    {
        public static WarehousesFilterDto ToDto (this WarehousesFilterViewModel source)
        {
            WarehousesFilterDto result = null;
            if (source != null)
            {
                result = new WarehousesFilterDto()
                {
                    Name = source.Name,
                    ErpReference = source.ERPReference,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result ?? new WarehousesFilterDto();
        }
    }
}