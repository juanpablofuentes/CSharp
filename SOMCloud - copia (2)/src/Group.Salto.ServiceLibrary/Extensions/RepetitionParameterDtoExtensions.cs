using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class RepetitionParameterDtoExtensions
    {
        public static RepetitionParameterDto ToDto(this RepetitionParameters source)
        {
            RepetitionParameterDto result = null;
            if (source != null)
            {
                result = new RepetitionParameterDto()
                {
                    Days = source.Days,
                    Id = source.Id,
                    IdCalculationType = source.IdCalculationType,
                    IdDamagedEquipment = source.IdDamagedEquipment,
                    IdDaysType = source.IdDaysType
                };
            }
            return result;
        }

        public static IList<RepetitionParameterDto> ToDto(this IList<RepetitionParameters> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static RepetitionParameters Update(this RepetitionParameters target, RepetitionParameterDto source)
        {
            if (target != null && source != null)
            {
                target.Days = source.Days;
                target.IdCalculationType = source.IdCalculationType;
                target.IdDamagedEquipment = source.IdDamagedEquipment;
                target.IdDaysType = source.IdDaysType;
            }
            return target;
        }

        public static RepetitionParameters ToEntity(this RepetitionParameterDto source)
        {
            RepetitionParameters result = null;
            if (source != null)
            {
                result = new RepetitionParameters()
                {
                    Id = source.Id,
                    Days = source.Days,
                    IdCalculationType = source.IdCalculationType,
                    IdDamagedEquipment = source.IdDamagedEquipment,
                    IdDaysType = source.IdDaysType
                };
            }
            return result;
        }
    }
}