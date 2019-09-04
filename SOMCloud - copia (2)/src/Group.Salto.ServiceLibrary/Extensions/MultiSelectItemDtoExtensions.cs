using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class MultiSelectItemDtoExtensions
    {
        public static PeoplePermissions ToPermissionsEntity(this MultiSelectItemDto source, int peopleId)
        {
            PeoplePermissions result = null;
            if (source != null)
            {
                result = new PeoplePermissions()
                {
                    PeopleId = peopleId,
                    PermissionId = Convert.ToInt16(source.Value),
                    AssignmentDate = DateTime.UtcNow
                };
            }

            return result;
        }

        public static IList<PeoplePermissions> ToPermissionsEntity(this IList<MultiSelectItemDto> source, int peopleId)
        {
            return source?.MapList(c => c.ToPermissionsEntity(peopleId));
        }

        public static ActionsRoles ToActionsRoleEntity(this MultiSelectItemDto source, int rolId)
        {
            ActionsRoles result = null;
            if (source != null)
            {
                result = new ActionsRoles()
                {
                    ActionId = Convert.ToInt16(source.Value),
                    RoleId = rolId.ToString()
                };
            }

            return result;
        }

        public static IList<ActionsRoles> ToActionsRoleEntity(this IList<MultiSelectItemDto> source, int rolId)
        {
            return source?.MapList(c => c.ToActionsRoleEntity(rolId));
        }
    }
}