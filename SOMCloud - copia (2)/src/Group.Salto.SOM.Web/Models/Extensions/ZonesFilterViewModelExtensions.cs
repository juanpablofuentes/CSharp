using Group.Salto.ServiceLibrary.Common.Dtos.Zones;
using Group.Salto.SOM.Web.Models.Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ZonesFilterViewModelExtensions
    {
        public static ZonesFilterDto ToDto(this ZonesFilterViewModel source)
        {
            ZonesFilterDto result = null;
            if (source != null)
            {
                result = new ZonesFilterDto()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}