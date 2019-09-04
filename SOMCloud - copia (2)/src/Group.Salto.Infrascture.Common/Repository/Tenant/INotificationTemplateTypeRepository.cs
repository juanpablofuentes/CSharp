using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface INotificationTemplateTypeRepository
    {
        NotificationTemplateType GetByName(string name);
    }
}
