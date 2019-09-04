using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPeoplePermissionsRepository
    {
        IList<PeoplePermissions> GetByPeopleId(int peopleId);
        int[] GetPermissionsIdByPeopleId(int peopleId);
    }
}