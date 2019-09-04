using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ITenantConfigurationRepository : IRepository<TenantConfiguration>
    {
        TenantConfiguration GetByKey(string key);
        TenantConfiguration GetByKey(string key, string connectionString);
        IQueryable<TenantConfiguration> GetByGroup(string group);
        IList<TenantConfiguration> GetByGroup(string group, string connectionString);
    }
}