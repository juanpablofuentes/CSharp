using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPeopleCollectionRepository : IRepository<PeopleCollections>
    {
        IQueryable<PeopleCollections> GetAll();
        PeopleCollections GetById(int id);
        SaveResult<PeopleCollections> UpdatePeopleCollection(PeopleCollections entity);
        SaveResult<PeopleCollections> CreatePeopleCollection(PeopleCollections entity);
        SaveResult<PeopleCollections> DeletePeopleCollection(PeopleCollections entity);
        Dictionary<int, string> GetAllKeyValuesByPeopleId(int peopleId);
    }
}