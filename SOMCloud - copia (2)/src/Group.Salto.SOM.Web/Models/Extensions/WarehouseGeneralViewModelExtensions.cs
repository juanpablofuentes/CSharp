using Group.Salto.ServiceLibrary.Common.Dtos.Warehouses;
using Group.Salto.SOM.Web.Models.Warehouses;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WarehouseGeneralViewModelExtensions
    {
        public static WarehouseGeneralViewModel ToViewModel(this WarehousesBaseDto source)
        {
            WarehouseGeneralViewModel result = null;
            if (source != null)
            {
                result = new WarehouseGeneralViewModel
                {
                    Id = source.Id,
                    Name = source.Name,
                    Code = source.Code,
                    ERPReference = source.ErpReference
                };
            }
            return result;
        }

        public static WarehousesBaseDto ToDto(this WarehouseGeneralViewModel source)
        {
            WarehousesBaseDto result = null;
            if (source != null)
            {
                result = new WarehousesBaseDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    Code = source.Code,
                    ErpReference = source.ERPReference
                };
            }
            return result;
        }
    }
}