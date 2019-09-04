using Group.Salto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IDamagedEquipmentRepository : IRepository<DamagedEquipment>
    {
        IQueryable<DamagedEquipment> GetAll();
        DamagedEquipment GetById(Guid id);
        List<string> GetAllNamesById(IList<Guid> IdDamagedEquipment);
        Dictionary<Guid, string> GetAllKeyValues();
    }
}