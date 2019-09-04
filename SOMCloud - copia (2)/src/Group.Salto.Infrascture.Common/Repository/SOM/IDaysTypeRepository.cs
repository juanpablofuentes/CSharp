
using Group.Salto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IDaysTypeRepository : IRepository<DaysType>
    {
        IQueryable<DaysType> GetAll();
        DaysType GetById(Guid id);
        List<string> GetAllNamesById(IList<Guid> ids);
        Dictionary<Guid, string> GetAllKeyValues();
    }
}