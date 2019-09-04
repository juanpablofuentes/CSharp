using Group.Salto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IWarehouseMovementTypesRepository : IRepository<WarehouseMovementTypes>
    {
        WarehouseMovementTypes GetById(Guid id);
        IQueryable<WarehouseMovementTypes> GetAll();
        Dictionary<Guid, string> GetAllKeyValues();
    }
}