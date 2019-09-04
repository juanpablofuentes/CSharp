using System;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class UserConfigurationRepository : BaseRepository<UserConfiguration>, IUserConfigurationRepository
    {
        public UserConfigurationRepository(ITenantUnitOfWork uow) : base(uow) { }

        public int GetLast()
        {
            return DbSet.Max(x => x.Id);
        }

        public SaveResult<UserConfiguration> CreateUserConfiguration(UserConfiguration userConfiguration)
        {
            Create(userConfiguration);
            SaveResult<UserConfiguration> result = SaveChange(userConfiguration);
            result.Entity = userConfiguration;

            return result;
        }

        public bool DeleteUserConfiguration(UserConfiguration userConfiguration)
        {
            Delete(userConfiguration);
            SaveResult<UserConfiguration> result = SaveChange(userConfiguration);
            result.Entity = userConfiguration;

            return result.IsOk;
        }

        public int? GetPeopleIdFromUserId(Guid userId)
        {
            return  Find(x => x.GuidId == userId)?.Id;
        }
    }
}