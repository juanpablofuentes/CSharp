using Group.Salto.ServiceLibrary.Common.Dtos.Rols;
using Group.Salto.SOM.Web.Models.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class RolFilterViewModelExtensions
    {
        public static RolFilterDto ToDto(this RolFilterViewModel source)
        {
            RolFilterDto result = null;

            if (source != null)
            {
                result = new RolFilterDto()
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