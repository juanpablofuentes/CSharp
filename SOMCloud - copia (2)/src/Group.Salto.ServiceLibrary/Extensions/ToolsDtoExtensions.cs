using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ToolsDtoExtensions
    {
        public static ToolsDto ToListDto(this Tools source)
        {
            ToolsDto result = null;
            if (source != null)
            {
                result = new ToolsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations,
                    VehicleName = source.Vehicle?.Name
                };
            }
            return result;
        }

        public static IList<ToolsDto> ToListDto(this IList<Tools> source)
        {
            return source.MapList(x => x.ToListDto());
        }

        public static Tools ToEntity(this ToolsDto source)
        {
            Tools result = null;
            if (source != null)
            {
                result = new Tools()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations,
                    
                };
            }

            return result;
        }  
    }
}