using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PeopleProjectsRepository : BaseRepository<PeopleProjects>, IPeopleProjectsRepository
    {
        public PeopleProjectsRepository(ITenantUnitOfWork uow) : base(uow)
        {

        }
   
        public PeopleProjects DeleteOnContext(PeopleProjects entity)
        {
            Delete(entity);
            return entity;
        }
    }
}