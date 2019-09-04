using Group.Salto.Entities.Tenant.ResultEntities;

namespace Group.Salto.Infrastructure.Common.Service
{
    public interface ITenantCreatorService
    {
        DatabaseCreation Create(string name);
        string GetDatabaseName(string name);
    }
}
