using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class UsagesRepository : BaseRepository<Usages>, IUsagesRepository
    {
        public UsagesRepository(ITenantUnitOfWork uow) : base(uow)
        { 
        }
        
        public Usages GetById(int id) 
        {
            return Find(x=> x.Id == id);
        }
    }
}