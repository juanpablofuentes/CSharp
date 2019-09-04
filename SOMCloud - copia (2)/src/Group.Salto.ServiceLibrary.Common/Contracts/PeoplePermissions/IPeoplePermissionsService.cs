using Group.Salto.ServiceLibrary.Common.Dtos.PeoplePermissions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.PeoplePermissions
{
    public interface IPeoplePermissionsService
    {
        IList<PeoplePermissionsDto> GetByPeopleId(int peopleId);
    }
}