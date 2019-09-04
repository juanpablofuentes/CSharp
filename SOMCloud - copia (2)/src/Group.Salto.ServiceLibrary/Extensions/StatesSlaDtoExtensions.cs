using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.StatesSla;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class StatesSlaDtoExtensions
    {
        public static StatesSlaDto ToDetailDto(this StatesSla source)
        {
            StatesSlaDto result = null;
            if (source != null)
            {
                result = new StatesSlaDto()
                {
                    Id = source.Id,
                    SlaId = source.SlaId,
                    MinutesToTheEnd = source.MinutesToTheEnd,
                    RowColor = source.RowColor
                };
            }
            return result;
        }

        public static StatesSlaDto ToStatesSlaDto(this StatesSla source)
        {
            StatesSlaDto result = null;
            if (source != null)
            {
                result = new StatesSlaDto
                {
                    Id = source.Id,
                    MinutesToTheEnd = source.MinutesToTheEnd,
                    RowColor = source.RowColor,
                    SlaId = source.SlaId
                };
            }

            return result;
        }

        public static IList<StatesSlaDto> ToStatesSlaDto(this IList<StatesSla> source)
        {
            return source?.MapList(sC => sC.ToStatesSlaDto());
        }

        public static StatesSla ToEntitySla(this StatesSlaDto source)
        {
            StatesSla result = null;
            if (source != null)
            {
                result = new StatesSla
                {
                    MinutesToTheEnd = source.MinutesToTheEnd,
                    RowColor = source.RowColor,
                };
            }

            return result;
        }

        public static IList<StatesSla> ToEntitySla(this IList<StatesSlaDto> source)
        {
            return source?.MapList(sC => sC.ToEntitySla());
        }
    }
}