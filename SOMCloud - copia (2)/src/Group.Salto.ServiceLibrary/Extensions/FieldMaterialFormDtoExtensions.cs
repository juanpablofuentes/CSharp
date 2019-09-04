using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class FieldMaterialFormDtoExtensions
    {
        public static FieldMaterialFormDto ToDto(this MaterialForm dbModel)
        {
            var dto = new FieldMaterialFormDto
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                SerialNumber = dbModel.SerialNumber,
                Reference = dbModel.Reference,
                Units = dbModel.Units
            };

            return dto;
        }

        public static IEnumerable<FieldMaterialFormDto> ToDto(this IEnumerable<MaterialForm> dbModelList)
        {
            var dtoList = new List<FieldMaterialFormDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }
    }
}
