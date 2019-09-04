using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.ToolsType;
using Group.Salto.SOM.Web.Models.ToolsType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ToolsTypeFilterViewModelExtensions
    {
        public static ToolsTypeFilterDto ToDto(this ToolsTypeFilterViewModel source)
        {
            ToolsTypeFilterDto result = null;
            if (source != null)
            {
                result = new ToolsTypeFilterDto()
                {
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                    Name = source.Name,
                    Description = source.Description
                };
            }

            return result;
        }
    }
}