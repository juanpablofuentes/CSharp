using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.ToolsType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ToolsTypeDtoExtensions
    {
        public static ToolsTypeDto ToListDto(this ToolsType source)
        {
            ToolsTypeDto result = null;
            if (source != null)
            {
                result = new ToolsTypeDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations,  
                };
            }
            return result;
        }

        public static IList<ToolsTypeDto> ToListDto(this IList<ToolsType> source)
        {
            return source.MapList(x => x.ToListDto());
        }

        public static ToolsType ToEntity(this ToolsTypeDto source)
        {
            ToolsType result = null;
            if (source != null)
            {
                result = new ToolsType()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations,                

                };
            }

            return result;
        }
        public static ToolsType Update(this ToolsType target, ToolsTypeDetailsDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
            }

            return target;
        }

    }
}