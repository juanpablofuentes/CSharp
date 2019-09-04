using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPeoplePushRegistrationRepository
    {
        PeoplePushRegistration GetByDeviceId(string deviceId);
        SaveResult<PeoplePushRegistration> CreateRegistration(PeoplePushRegistration pushRegistration);
        SaveResult<PeoplePushRegistration> UpdateRegistration(PeoplePushRegistration pushRegistration);
        IQueryable<PeoplePushRegistration> GetByPeopleIdEnabled(int peopleId);
        IQueryable<PeoplePushRegistration> GetByPeopleId(int peopleId);
        IQueryable<PeoplePushRegistration> GetByPeopleIdsEnabled(IEnumerable<int> peopleIds);
    }
}
