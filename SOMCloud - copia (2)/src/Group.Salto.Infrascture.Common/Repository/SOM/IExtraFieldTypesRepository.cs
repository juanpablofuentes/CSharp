using Group.Salto.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IExtraFieldTypesRepository : IRepository<ExtraFieldTypes>
    {
        ExtraFieldTypes GetById(int id);
        IQueryable<ExtraFieldTypes> GetAll();
        Dictionary<int, string> GetAllKeyValues();
    }
}