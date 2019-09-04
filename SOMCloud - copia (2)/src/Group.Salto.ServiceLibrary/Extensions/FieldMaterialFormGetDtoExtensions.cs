using System.Collections.Generic;
using Group.Salto.Entities.Tenant.QueryEntities;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class FieldMaterialFormGetDtoExtensions
    {
        public static IEnumerable<FieldMaterialFormGetDto> ToDto(this IEnumerable<FieldMaterialForm> sourceList)
        {
            var result = new List<FieldMaterialFormGetDto>();

            foreach (var source in sourceList)
            {
                result.Add(source.ToDto());
            }

            return result;
        }

        public static FieldMaterialFormGetDto ToDto(this FieldMaterialForm source)
        {
            FieldMaterialFormGetDto result = null;
            if (source != null)
            {
                result = new FieldMaterialFormGetDto
                {
                    Description = source.Description,
                    MaxUnits = source.MaxUnits,
                    Reference = source.Reference,
                    SerialNumber = source.SerialNumber,
                    Units = source.Units
                };

            }
            return result;
        }
    }
}
