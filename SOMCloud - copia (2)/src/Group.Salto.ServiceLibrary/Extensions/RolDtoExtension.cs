using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Rols;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class RolDtoExtension
    {
        public static RolDto ToDto(this Roles source)
        {
            RolDto result = null;

            if (source != null)
            {
                result = new RolDto()
                {
                    Id = System.Convert.ToInt32(source.Id),
                    Name = source.Name,
                    Description = source.Description
                };
            }

            return result;
        }

        public static IList<RolDto> ToDto(this IList<Roles> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static Roles ToEntity(this RolDto source)
        {
            Roles result = null;
            if (source != null)
            {
                result = new Roles()
                {
                    Id = source.Id.ToString(),
                    Name = source.Name,
                    NormalizedName = source.Name.ToUpper(),
                    Description = source.Description,
                    //TODO: Carmen. RolesActionGroupsActions
                    ActionsRoles = source.ActionsRoles.ToList().Where(x => x.IsChecked).ToList().ToActionsRoleEntity(source.Id)
                };
            }

            return result;
        }

        public static void UpdateRolEntity(this Roles target, Roles source)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.NormalizedName = source.NormalizedName;
                target.Description = source.Description;
                //TODO: Carmen. RolesActionGroupsActions
                //target.ActionsRoles = source.ActionsRoles;
            }
        }

        public static bool IsValid(this RolDto source)
        {
            bool result = false;
            result = source != null 
                && !string.IsNullOrEmpty(source.Name)
                && !string.IsNullOrEmpty(source.Description);
            return result;
        }
    }
}
