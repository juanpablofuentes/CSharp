using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Warehouses;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Warehouses
{
    public interface IWarehousesService
    {
        ResultDto<IList<WarehousesBaseDto>> GetAllFiltered(WarehousesFilterDto filter);
        ResultDto<WarehousesBaseDto> CreateWarehouse(WarehousesBaseDto warehouse);
        ResultDto<WarehousesBaseDto> GetById(int id);
        ResultDto<WarehousesBaseDto> UpdateWarehouse(WarehousesBaseDto warehouse);
        ResultDto<bool> DeleteWarehouse(int id);
        ResultDto<ErrorDto> CanDelete(int id);
    }
}