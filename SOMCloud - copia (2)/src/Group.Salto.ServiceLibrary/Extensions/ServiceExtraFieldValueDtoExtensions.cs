using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ServiceExtraFieldValueDtoExtensions
    {
        public static ExtraFieldValueDto ToDto(this ExtraFields dbModel)
        {
            var dto = new ExtraFieldValueDto
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Description = dbModel.Description,
                Observations = dbModel.Observations,
                Type = (ExtraFieldValueTypeEnum)dbModel.Type,
                AllowedStringValues = dbModel.AllowedStringValues,
                DelSystem = dbModel.DelSystem,
                MultipleChoice = dbModel.MultipleChoice,
                IsMandatory = dbModel.IsMandatory,
                ErpSystemInstanceQueryId = dbModel.ErpSystemInstanceQueryId
            };

            return dto;
        }

        public static IEnumerable<ExtraFieldValueDto> ToDto(this IEnumerable<ExtraFields> dbModelList)
        {
            var dtoList = new List<ExtraFieldValueDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }

        public static ServiceExtraFieldValueDto ToDto(this ExtraFieldsValues dbModel)
        {
            var dto = new ServiceExtraFieldValueDto
            {
                Id = dbModel.Id,
                BooleaValue = dbModel.BooleaValue,
                DateValue = dbModel.DataValue,
                DecimalValue = dbModel.DecimalValue,
                EnterValue = dbModel.EnterValue,
                Signature = dbModel.Signature,
                StringValue = dbModel.StringValue,
                ExtraField = dbModel.ExtraField?.ToDto(),
                MaterialForms = dbModel.MaterialForm?.ToDto()
            };

            return dto;
        }

        public static IEnumerable<ServiceExtraFieldValueDto> ToDto(this IEnumerable<ExtraFieldsValues> dbModelList)
        {
            var dtoList = new List<ServiceExtraFieldValueDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }
    }
}
