using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class NotificationTemplateTypeRepository : BaseRepository<NotificationTemplateType>, INotificationTemplateTypeRepository
    {
        public NotificationTemplateTypeRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public NotificationTemplateType GetByName(string name)
        {
            return Find(n => n.Name == name);
        }
    }
}
