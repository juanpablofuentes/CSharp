using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.ReferenceTimeSla;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ReferenceTimeSlaDtoExtensions
    {
        public static ReferenceTimeSlaDto ToDto(this ReferenceTimeSla source)
        {
            ReferenceTimeSlaDto result = null;
            if (source != null)
            {
                result = new ReferenceTimeSlaDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source?.Description
                };
            }
            return result;
        }

        public static IList<ReferenceTimeSlaDto> ToDto(this IList<ReferenceTimeSla> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}