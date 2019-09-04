using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter;
using System.Text;
using Group.Salto.Common.Helpers;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class RepetitionParameterDetailDtoExtensions
    {
        public static RepetitionParametersDetailDto ToDetailDto(this RepetitionParameters source)
        {
            RepetitionParametersDetailDto result = null;
            if (source != null)
            {
                result = new RepetitionParametersDetailDto()
                {
                    Id = source.Id,
                    Days = source.Days,
                    IdCalculationType = source.IdCalculationType,
                    IdDamagedEquipment = source.IdDamagedEquipment,
                    IdDaysType = source.IdDaysType,
                };
            }
            return result;
        }

        public static IList<RepetitionParametersDetailDto> ToListDetailDto(this IList<RepetitionParameters> source)
        {
            return source?.MapList(x => x.ToDetailDto());
        }

        public static RepetitionParameters Update(this RepetitionParameters target,  RepetitionParametersDetailDto source)
        {
            if (source != null && target != null)
            {

                target.Days = source.Days;
                target.IdCalculationType = source.IdCalculationType;
                target.IdCalculationType = source.IdCalculationType;
                target.IdDaysType = source.IdDaysType;                
            }
            return target;
        }
    }
}