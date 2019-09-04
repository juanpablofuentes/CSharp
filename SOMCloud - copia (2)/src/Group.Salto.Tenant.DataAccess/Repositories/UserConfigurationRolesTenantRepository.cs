using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class UserConfigurationRolesTenantRepository : BaseRepository<UserConfigurationRolesTenant>, IUserConfigurationRolesTenantRepository
    {
        public UserConfigurationRolesTenantRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public UserConfigurationRolesTenant GetByUserConfigurationId(int userConfigurationId)
        {
            return Find(x => x.UserConfigurationId == userConfigurationId);
        }

        public string GetRoleIdByUserId(int userConfigurationId)
        {
            var res = Find(x => x.UserConfigurationId == userConfigurationId);
            return res != null ? res.RolesTenantId : string.Empty;
        }

        public SaveResult<UserConfigurationRolesTenant> CreateUserConfigurationRolTenant(UserConfigurationRolesTenant userConfigurationRolesTenant)
        {
            Create(userConfigurationRolesTenant);
            var result = SaveChange(userConfigurationRolesTenant);
            return result;
        }

        public SaveResult<UserConfigurationRolesTenant> DeleteUserConfigurationRolTenant(UserConfigurationRolesTenant entity)
        {
            Delete(entity);
            var result = SaveChange(entity);
            return result;
        }
    }
}