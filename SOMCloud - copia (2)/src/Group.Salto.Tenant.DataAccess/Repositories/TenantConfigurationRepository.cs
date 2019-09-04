using DataAccess.Common.Repositories;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class TenantConfigurationRepository : ExplicitBaseRepository<TenantConfiguration>, ITenantConfigurationRepository
    {
        public TenantConfigurationRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<TenantConfiguration> GetByGroup(string group)
        {
            return Filter(x => x.Group == group);
        }

        public TenantConfiguration GetByKey(string key)
        {
            return Find(x => x.Key == key);
        }

        public TenantConfiguration GetByKey(string key, string connectionString)
        {
            CreateInstance(connectionString);
            var result = Find(x => x.Key == key);
            DestroyInstance();
            return result;
        }

        public IList<TenantConfiguration> GetByGroup(string group, string connectionString)
        {
            CreateInstance(connectionString);
            var result = Filter(x => x.Group == group).ToList();
            DestroyInstance();
            return result;
        }
    }
}