using Group.Salto.ServiceLibrary.Common.Dtos.Sla;
using Group.Salto.SOM.Web.Models.Sla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SlaFilterViewModelExtensions
    {
        public static SlaFilterDto ToDto(this SlaFilterViewModel source)
        {
            SlaFilterDto result = null;
            if (source != null)
            {
                result = new SlaFilterDto()
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