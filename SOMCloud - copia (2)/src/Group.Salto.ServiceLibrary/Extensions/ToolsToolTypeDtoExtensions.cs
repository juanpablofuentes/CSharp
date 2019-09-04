using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ToolsToolTypeDtoExtensions
    {
        public static BaseNameIdDto<int> ToToolsToolTypeDto(this ToolsToolTypes source)
        {
            BaseNameIdDto<int> result = null;
            if (source != null)
            {
                result = new BaseNameIdDto<int>()
                {
                    Name = source.ToolType.Name,
                    Id = source.ToolType.Id
                };
            }

            return result;
        }

        public static IList<BaseNameIdDto<int>> ToToolsToolTypeDto(this IList<ToolsToolTypes> source)
        {
            return source?.MapList(sC => sC.ToToolsToolTypeDto());
        }
    }
}