using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.RolesTenant;
using Group.Salto.SOM.Web.Models.RolTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class RolTenantViewModelExtensions
    {
        public static RolTenantViewModel ToRolTenantViewModel(this RolTenantListDto source)
        {
            RolTenantViewModel result = null;
            if (source != null)
            {
                result = new RolTenantViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }

        public static RolTenantViewModel ToRolTenantViewModel(this RolTenantDto source)
        {
            RolTenantViewModel result = null;
            if (source != null)
            {
                result = new RolTenantViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description
                    //TODO Carmen. Falta el mapeo de la ternaria
                };
            }
            return result;
        }

        public static RolTenantDto ToRolTenantDto(this RolTenantViewModel source)
        {
            RolTenantDto result = null;
            if (source != null)
            {
                result = new RolTenantDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,

                };
            }
            return result;
        }

        public static IEnumerable<RolTenantViewModel> ToRolTenantListViewModel(this IEnumerable<RolTenantListDto> source)
        {
            return source?.MapList(x => x.ToRolTenantViewModel());
        }        

        public static IEnumerable<RolTenantViewModel> ToRolTenantListViewModel(this IEnumerable<RolTenantDto> source)
        {
            return source?.MapList(x => x.ToRolTenantViewModel());
        }
    }
}