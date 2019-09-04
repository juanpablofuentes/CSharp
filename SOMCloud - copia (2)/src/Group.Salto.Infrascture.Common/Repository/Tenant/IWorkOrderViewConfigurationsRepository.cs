using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkOrderViewConfigurationsRepository : IRepository<UsersMainWoviewConfigurations>
    {
        UsersMainWoviewConfigurations GetDefaultConfigByUserId(int userId);
        List<UsersMainWoviewConfigurations> GetAllViewsByUserId(int userId);
        UsersMainWoviewConfigurations GetColumnsByViewId(int id);
        UsersMainWoviewConfigurations GetViewColumnsByViewId(int id);
        SaveResult<UsersMainWoviewConfigurations> CreateViewConfiguration(UsersMainWoviewConfigurations Configuration);
        SaveResult<UsersMainWoviewConfigurations> UpdateViewConfiguration(UsersMainWoviewConfigurations Configuration);
        SaveResult<UsersMainWoviewConfigurations> DeleteViewConfiguration(UsersMainWoviewConfigurations Configuration);
    }
}