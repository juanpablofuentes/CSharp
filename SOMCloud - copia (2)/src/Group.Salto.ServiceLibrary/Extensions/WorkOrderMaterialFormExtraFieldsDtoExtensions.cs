using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderMaterialFormExtraFieldsDtoExtensions
    {
        public static FieldMaterialFormDto ToMaterialFormDto(this MaterialForm source)
        {
            FieldMaterialFormDto result = null;
            if(source != null)
            {
                result = new FieldMaterialFormDto()
                {
                    Id = source.Id,
                    SerialNumber = source.SerialNumber,
                    Description = source.Description,
                    Reference = source.Reference,
                    Units = source.Units,
                };
            }
            return result;
        }

        public static IList<FieldMaterialFormDto> ToMaterialFormDto(this IList<MaterialForm> source)
        {
            return source?.MapList(x => x.ToMaterialFormDto());
        }
    }
}