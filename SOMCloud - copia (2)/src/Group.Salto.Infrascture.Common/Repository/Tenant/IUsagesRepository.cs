using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IUsagesRepository : IRepository<Usages>
    {        
        Usages GetById(int id);
    }
}