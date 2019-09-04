using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IMainWoViewConfigurationsColumnsRepository : IRepository<MainWoViewConfigurationsColumns>
    {
        IQueryable<MainWoViewConfigurationsColumns> GetFilterValuesByColumnId(int id);
    }
}