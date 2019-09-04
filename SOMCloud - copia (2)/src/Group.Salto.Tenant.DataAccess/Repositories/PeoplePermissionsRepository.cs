using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PeoplePermissionsRepository : BaseRepository<PeoplePermissions>, IPeoplePermissionsRepository
    {
        public PeoplePermissionsRepository(ITenantUnitOfWork uow) : base(uow) { }

        public IList<PeoplePermissions> GetByPeopleId(int peopleId)
        {
            return All().Where(x => x.PeopleId == peopleId).ToList();
        }

        public int[] GetPermissionsIdByPeopleId(int peopleId)
        {
            return Filter(x => x.PeopleId == peopleId).Select(x => x.PermissionId).ToArray();
        }
    }
}