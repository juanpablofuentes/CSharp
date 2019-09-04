using System;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IUserConfigurationRepository : IRepository<UserConfiguration>
    {
        int GetLast();
        SaveResult<UserConfiguration> CreateUserConfiguration(UserConfiguration userConfiguration);
        bool DeleteUserConfiguration(UserConfiguration userConfiguration);
        int? GetPeopleIdFromUserId(Guid userId);
    }
}