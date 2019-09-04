using System.Collections.Generic;
using System.Linq;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPredefinedServiceRepository : IRepository<PredefinedServices>
    {
        IQueryable<PredefinedServices> GetAll();
        IQueryable<PredefinedServices> GetAllById(IList<int> ids);
        Dictionary<int, string> GetAllKeyValuesWithProjects();
        PredefinedServices GetByIdForCanDelete(int id);
        PredefinedServices DeleteOnContext(PredefinedServices entity);
        PredefinedServices GetById(int id);
        PredefinedServices GetByIdIncludeExtraFields(int id);
    }
}