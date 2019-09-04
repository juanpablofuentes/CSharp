using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;

namespace Group.Salto.DataAccess.Repositories
{
    public class WarehouseMovementTypesRepository : BaseRepository<WarehouseMovementTypes>, IWarehouseMovementTypesRepository
    {
        public WarehouseMovementTypesRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<WarehouseMovementTypes> GetAll()
        {
            return All();
        }

        public Dictionary<Guid, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(x => x.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public WarehouseMovementTypes GetById(Guid id)
        {
             return Find(x => x.Id == id);
        }
    }
}