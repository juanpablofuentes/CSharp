using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovements;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WarehouseMovements
{
    public interface IWarehouseMovementsService
    {
        ResultDto<IList<WarehouseMovementsDto>> GetAllFiltered(WarehouseMovementsFilterDto filter);
        ResultDto<WarehouseMovementsDto> CreateWarehouseMovement(WarehouseMovementsDto movement);
    }
}