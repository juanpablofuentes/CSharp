using Group.Salto.ServiceLibrary.Common.Dtos.RolesTenant;
using Group.Salto.SOM.Web.Models.RolTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class RolTenantFilterViewModelExtensions
    {
        public static RolTenantFilterDto ToDto(this RolTenantFilterViewModel source)
        {
            RolTenantFilterDto result = null;
            if (source != null)
            {
                result = new RolTenantFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy
                };
            }
            return result;
        }
    }
}