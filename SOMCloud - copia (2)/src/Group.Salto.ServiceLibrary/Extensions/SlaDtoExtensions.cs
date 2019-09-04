using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Sla;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SlaDtoExtensions
    {
        public static SlaDto ToDto(this Sla source)
        {
            SlaDto result = null;
            if (source != null)
            {
                result = new SlaDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    MinutesResponse = source.MinutesResponse,
                    MinutesResolutions = source.MinutesResolutions,
                    MinutesUnansweredPenalty = source.MinutesUnansweredPenalty,
                    MinutesPenaltyWithoutResolution = source.MinutesPenaltyWithoutResolution
                };
            }
            return result;
        }

        public static IList<SlaDto> ToDto(this IList<Sla> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}