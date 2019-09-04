using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPeopleNotificationRepository
    {
        SaveResult<PeopleNotification> CreatePeopleNotification(PeopleNotification peopleNotification);
        IQueryable<PeopleNotification> GetByPeopleIncludeTranslations(int peopleId);
        void CreatePeopleNotificationWithoutSave(PeopleNotification newNotification);
    }
}
