using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.PeoplePermissions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PeoplePermissionsDtoExtensions
    {
        public static PeoplePermissionsDto ToDto(this PeoplePermissions source)
        {
            PeoplePermissionsDto result = null;
            if (source != null)
            {
                result = new PeoplePermissionsDto()
                {
                    PeopleId = source.PeopleId,
                    PermissionId = source.PermissionId,
                    AssignmentDate = source.AssignmentDate
                };
            }

            return result;
        }

        public static PeoplePermissions ToEntity(this PeoplePermissionsDto source)
        {
            PeoplePermissions result = null;
            if (source != null)
            {
                result = new PeoplePermissions()
                {
                    PeopleId = source.PeopleId,
                    PermissionId = source.PermissionId,
                    AssignmentDate = source.AssignmentDate
                };
            }

            return result;
        }

        public static IList<PeoplePermissionsDto> ToDto(this IList<PeoplePermissions> source)
        {
            return source?.MapList(c => c.ToDto());
        }

        public static IList<PeoplePermissions> ToEntity(this IList<PeoplePermissionsDto> source)
        {
            return source?.MapList(c => c.ToEntity());
        }
    }
}
