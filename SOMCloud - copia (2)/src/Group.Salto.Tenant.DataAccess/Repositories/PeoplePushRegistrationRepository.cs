using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PeoplePushRegistrationRepository : BaseRepository<PeoplePushRegistration>, IPeoplePushRegistrationRepository
    {
        public PeoplePushRegistrationRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public PeoplePushRegistration GetByDeviceId(string deviceId)
        {
            return Filter(p => p.DeviceId == deviceId).FirstOrDefault();
        }

        public SaveResult<PeoplePushRegistration> CreateRegistration(PeoplePushRegistration pushRegistration)
        {
            Create(pushRegistration);
            var result = SaveChange(pushRegistration);
            return result;
        }

        public SaveResult<PeoplePushRegistration> UpdateRegistration(PeoplePushRegistration pushRegistration)
        {
            Update(pushRegistration);
            var result = SaveChange(pushRegistration);
            return result;
        }

        public IQueryable<PeoplePushRegistration> GetByPeopleIdEnabled(int peopleId)
        {
            return Filter(r => r.PeopleId == peopleId && r.Enabled);
        }

        public IQueryable<PeoplePushRegistration> GetByPeopleId(int peopleId)
        {
            return Filter(r => r.PeopleId == peopleId);
        }

        public IQueryable<PeoplePushRegistration> GetByPeopleIdsEnabled(IEnumerable<int> peopleIds)
        {
            return Filter(r => peopleIds.Contains(r.PeopleId) && r.Enabled);
        }
    }
}
