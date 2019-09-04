using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WorkOrderViewConfigurationsRepository : BaseRepository<UsersMainWoviewConfigurations>, IWorkOrderViewConfigurationsRepository
    {
        public WorkOrderViewConfigurationsRepository(ITenantUnitOfWork uow) : base(uow) { }

        public UsersMainWoviewConfigurations GetDefaultConfigByUserId(int userId)
        {
            return Find(x => (x.IsDefault.HasValue && x.IsDefault.Value) && x.UserConfigurationId == userId);
        }

        public List<UsersMainWoviewConfigurations> GetAllViewsByUserId(int userId)
        {
            return Filter(x => x.UserConfigurationId == userId).ToList();
        }

        public UsersMainWoviewConfigurations GetColumnsByViewId(int id)
        {
            return Filter(x => x.Id == id)
                    .Include(x => x.MainWoViewConfigurationsColumns)
                    .FirstOrDefault();
        }

        public UsersMainWoviewConfigurations GetViewColumnsByViewId(int id)
        {
            return Filter(x => x.Id == id)
                    .Include(x => x.MainWoViewConfigurationsColumns)
                    .Include(x => x.MainWoViewConfigurationsPeople)
                    .Include(x => x.MainWoviewConfigurationsGroups)
                        .ThenInclude(x => x.PeopleCollection)
                            .ThenInclude(x => x.PeopleCollectionsPeople)
                    .FirstOrDefault();
        }

        public SaveResult<UsersMainWoviewConfigurations> CreateViewConfiguration(UsersMainWoviewConfigurations Configuration)
        {
            Configuration.UpdateDate = DateTime.UtcNow;
            Create(Configuration);
            var result = SaveChange(Configuration);
            return result;
        }

        public SaveResult<UsersMainWoviewConfigurations> UpdateViewConfiguration(UsersMainWoviewConfigurations Configuration)
        {
            Configuration.UpdateDate = DateTime.UtcNow;
            Update(Configuration);
            var result = SaveChange(Configuration);
            return result;
        }

        public SaveResult<UsersMainWoviewConfigurations> DeleteViewConfiguration(UsersMainWoviewConfigurations Configuration)
        {
            Configuration.UpdateDate = DateTime.UtcNow;
            Delete(Configuration);
            SaveResult<UsersMainWoviewConfigurations> result = SaveChange(Configuration);
            result.Entity = Configuration;
            return result;
        }
    }
}