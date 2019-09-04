using Group.Salto.ServiceLibrary.Common.Dtos.Tools;
using Group.Salto.SOM.Web.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ToolsFilterViewModelExtensions
    {
        public static ToolsFilterDto ToDto(this ToolsFilterViewModel source)
        {
            ToolsFilterDto result = null;
            if (source != null)
            {
                result = new ToolsFilterDto()
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