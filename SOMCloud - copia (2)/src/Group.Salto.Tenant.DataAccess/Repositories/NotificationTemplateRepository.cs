using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class NotificationTemplateRepository : BaseRepository<NotificationTemplate>, INotificationTemplateRepository
    {
        public NotificationTemplateRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public NotificationTemplate GetByTypeIncludeTranslations(int notificationType)
        {
            return Filter(n => n.PeopleNotificationTemplateTypeId == notificationType)
                .Include(n => n.NotificationTemplateTranslations).FirstOrDefault();
        }
    }
}
