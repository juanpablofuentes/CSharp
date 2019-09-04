using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ExtraFieldsTypesDtoExtensions
    {
        public static ExtraFieldsTypesDto ToDto(this ExtraFieldTypes source)
        {
            ExtraFieldsTypesDto result = null;
            if (source != null)
            {
                result = new ExtraFieldsTypesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    AllowedValuesVisibility = source.AllowedValuesVisibility,
                    IsMandatoryVisibility = source.IsMandatoryVisibility,
                    ErpSystemVisibility = source.ErpSystemVisibility,
                    MultipleChoiceVisibility = source.MultipleChoiceVisibility
                };
            }
            return result;
        }

        public static IList<ExtraFieldsTypesDto> ToDto(this IList<ExtraFieldTypes> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}