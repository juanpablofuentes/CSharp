using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.ToolsType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Group.Salto.Common.Helpers;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ToolsTypeDetailsDtoExtensions
    {
        public static ToolsTypeDetailsDto ToDetailDto(this ToolsType source)
        {
            ToolsTypeDetailsDto result = null;
            if (source != null)
            {
                result = new ToolsTypeDetailsDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Observations = source.Observations,
                    KnowledgeSelected = source.KnowledgeToolsType?.ToList().ToToolsTypeKnowledgeDto()
                    
                };
            }
            return result;
        }

        public static ToolsType ToEntity(this ToolsTypeDetailsDto source)
        {
            ToolsType result = null;
            if (source != null)
            {
                result = new ToolsType();
                source.ToEntity(result);
            }
            return result;
        }

        public static void ToEntity(this ToolsTypeDetailsDto source, ToolsType target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Id = source.Id;
                target.Observations = source.Observations;
                
            }
        }
    }
}