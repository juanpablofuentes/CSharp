using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ICollectionsClosureCodesRepository : IRepository<CollectionsClosureCodes>
    {
        IQueryable<CollectionsClosureCodes> GetAll();
        CollectionsClosureCodes GetById(int id);
        SaveResult<CollectionsClosureCodes> CreateClosureCode(CollectionsClosureCodes entity);
        SaveResult<CollectionsClosureCodes> UpdateClosureCode(CollectionsClosureCodes entity);
        SaveResult<CollectionsClosureCodes> DeleteClosureCode(CollectionsClosureCodes entity);
        Dictionary<int, string> GetAllKeyValues();
    }
}