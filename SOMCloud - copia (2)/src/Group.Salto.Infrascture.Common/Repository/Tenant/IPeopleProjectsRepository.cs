using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPeopleProjectsRepository : IRepository<PeopleProjects>
    {
        PeopleProjects DeleteOnContext(PeopleProjects entity);
    }
}