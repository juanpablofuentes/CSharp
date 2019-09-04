using Group.Salto.ServiceLibrary.Common.Dtos.PointRate;
using Group.Salto.SOM.Web.Models.PointRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PointRateFilterViewModelExtensions
    {
        public static PointRateFilterDto ToDto(this PointRateFilterViewModel source)
        {
            PointRateFilterDto result = null;
            if (source != null)
            {
                result = new PointRateFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}
