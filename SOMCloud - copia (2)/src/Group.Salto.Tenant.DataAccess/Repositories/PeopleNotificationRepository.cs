using System;
using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PeopleNotificationRepository : BaseRepository<PeopleNotification>, IPeopleNotificationRepository
    {
        public PeopleNotificationRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public SaveResult<PeopleNotification> CreatePeopleNotification(PeopleNotification peopleNotification)
        {
            peopleNotification.UpdateDate = DateTime.UtcNow;
            Create(peopleNotification);
            var result = SaveChange(peopleNotification);
            return result;
        }

        public IQueryable<PeopleNotification> GetByPeopleIncludeTranslations(int peopleId)
        {
            return Filter(n => n.PeopleId == peopleId).Include(n => n.PeopleNotificationTranslations).OrderByDescending(n => n.UpdateDate);
        }

        public void CreatePeopleNotificationWithoutSave(PeopleNotification newNotification)
        {
            newNotification.UpdateDate = DateTime.UtcNow;
            Create(newNotification);
        }
    }
}
