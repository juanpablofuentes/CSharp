using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Vehicles;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Vehicles
{
    public interface IVehiclesService
    {
        ResultDto<VehiclesDetailsDto> GetById(int id);
        ResultDto<VehiclesDetailsDto> UpdateVehicle(VehiclesDetailsDto source);
        ResultDto<IList<VehiclesDto>> GetAllFiltered(VehiclesFilterDto filter);
        ResultDto<VehiclesDetailsDto> CreateVehicle(VehiclesDetailsDto source);
        ResultDto<bool> DeleteVehicle(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}