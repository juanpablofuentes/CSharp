using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ExtraFieldsDtoExtension
    {
        public static ExtraFieldsDto ToListDto(this ExtraFields source)
        {
            ExtraFieldsDto result = null;
            if (source != null)
            {
                result = new ExtraFieldsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    TypeId = source.Type,
                    ErpSystemInstanceQueryId = source.ErpSystemInstanceQueryId,
                    ErpSystemInstanceQueryName = source.ErpSystemInstanceQuery?.Name,
                    AllowedStringValues = source.AllowedStringValues,
                    MultipleChoice = source.MultipleChoice,
                    IsMandatory = source.IsMandatory,
                    DelSystem = source.DelSystem,
                };

            }
            return result;
        }

        public static IList<ExtraFieldsDto> ToListDto(this IList<ExtraFields> source)
        {
            return source?.MapList(x => x.ToListDto());
        }
    }
}