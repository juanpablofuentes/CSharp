using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface INotificationTemplateRepository
    {
        NotificationTemplate GetByTypeIncludeTranslations(int notificationType);
    }
}
