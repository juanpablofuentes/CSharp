using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WoServiceDtoExtensions
    {
        public static WoServiceDto ToWoServiceDto(this Services dbModel)
        {
            var dto = new WoServiceDto
            {
                Id = dbModel.Id,
                Observations = dbModel.Observations,
                Description = dbModel.Description,
                Latitude = dbModel.Latitude,
                Longitude = dbModel.Longitude,
                ExtraFieldsValues = dbModel.ExtraFieldsValues?.ToDto(),
                CreationDate = dbModel.CreationDate,
                Cancelled = dbModel.Cancelled,
                PeopleResponsibleId = dbModel.PeopleResponsibleId,
                PredefinedService = dbModel.PredefinedService?.ToDto(),
                ClosingCodeId = dbModel.ClosingCodeId,
                DeliveryNote = dbModel.DeliveryNote,
                DeliveryProcessInit = dbModel.DeliveryProcessInit,
                FormState = dbModel.FormState,
                IdentifyExternal = dbModel.IdentifyExternal,
                IdentifyInternal = dbModel.IdentifyInternal,
                ServiceStateId = dbModel.ServiceStateId,
                ServicesCancelFormId = dbModel.ServicesCancelFormId,
                SubcontractResponsibleId = dbModel.SubcontractResponsibleId,
                ClosingCodes = dbModel.ClosingCode.ToClosingCodesFatherDto(),
                PeopleResponsible = dbModel.PeopleResponsible.ToDto()
            };

            return dto;
        }

        public static IEnumerable<WoServiceDto> ToWoServiceDto(this IEnumerable<Services> dbModelList)
        {
            var dtoList = new List<WoServiceDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToWoServiceDto());
            }

            return dtoList;
        }
    }
}
