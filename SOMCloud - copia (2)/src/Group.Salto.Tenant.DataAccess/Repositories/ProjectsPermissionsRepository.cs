using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ProjectsPermissionsRepository : BaseRepository<ProjectsPermissions>, IProjectsPermissionsRepository
    {
        public ProjectsPermissionsRepository(ITenantUnitOfWork uow) : base(uow)
        {

        }

        public IList<ProjectsPermissions> GetByProjectId(int projectId)
        {
            return Filter(x => x.ProjectId == projectId).ToList();
        }
    }
}