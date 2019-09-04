using System.Collections.Generic;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IVehiclesRepository
    {
        Vehicles GetById(int id);
        SaveResult<Vehicles> CreateVehicle(Vehicles vehicle);
        SaveResult<Vehicles> UpdateVehicle(Vehicles vehicle);
        SaveResult<Vehicles> DeleteVehicle(Vehicles vehicle);
        IQueryable<Vehicles> GetAllNotDeleted();
        Dictionary<int, string> GetAllKeyValues();
        IQueryable<Vehicles> GetByPeopleId(int peopleId);
    }
}