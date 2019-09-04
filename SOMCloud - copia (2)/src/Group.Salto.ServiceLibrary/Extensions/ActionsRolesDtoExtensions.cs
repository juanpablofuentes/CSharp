using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.ActionsRoles;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ActionsRolesDtoExtensions
    {
        //TODO: Carmen. RolesActionGroupsActions
        public static ActionsRolesDto ToDto(this ActionsRoles source)
        {
            ActionsRolesDto result = null;
            if (source != null)
            {
                result = new ActionsRolesDto()
                {
                    ActionId = source.ActionId,
                    RoleId = Convert.ToInt32(source.RoleId)
                };
            }

            return result;
        }

        public static ActionsRoles ToEntity(this ActionsRolesDto source)
        {
            ActionsRoles result = null;
            if (source != null)
            {
                result = new ActionsRoles()
                {
                    ActionId = source.ActionId,
                    RoleId = source.RoleId.ToString()
                };
            }

            return result;
        }

        public static IList<ActionsRolesDto> ToDto(this IList<ActionsRoles> source)
        {
            return source?.MapList(c => c.ToDto());
        }

        public static IList<ActionsRoles> ToEntity(this IList<ActionsRolesDto> source)
        {
            return source?.MapList(c => c.ToEntity());
        }
    }
}