using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class MainWoViewConfigurationsColumnsRepository : BaseRepository<MainWoViewConfigurationsColumns>, IMainWoViewConfigurationsColumnsRepository
    {
        public MainWoViewConfigurationsColumnsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<MainWoViewConfigurationsColumns> GetFilterValuesByColumnId(int id)
        {
            var result = Filter(x => x.ColumnId == id);

            return result;
        }
    }
}