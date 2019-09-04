using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;

using Group.Salto.ServiceLibrary.Common.Dtos.ToolsType;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ToolsTypeKnowledgeDtoExtensions
    {
        public static ToolsTypeKnowledgeDto ToToolsTypeKnowledgeDto(this KnowledgeToolsType source)
        {
            ToolsTypeKnowledgeDto result = null;
            if (source != null)
            {
                result = new ToolsTypeKnowledgeDto()
                {
                    Name = source.Knowledge.Name,
                    Id = source.Knowledge.Id,
                };
            }

            return result;
        }

        public static IList<ToolsTypeKnowledgeDto> ToToolsTypeKnowledgeDto(this IList<KnowledgeToolsType> source)
        {
            return source?.MapList(sC => sC.ToToolsTypeKnowledgeDto());
        }
    }
}