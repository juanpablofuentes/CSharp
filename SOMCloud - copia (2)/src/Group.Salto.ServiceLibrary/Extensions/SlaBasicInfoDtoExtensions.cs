using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Sla;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SlaBasicInfoDtoExtensions
    {
        public static SlaBasicInfoDto ToSlaBasicInfoDto(this Sla dbModel)
        {
            var dto = new SlaBasicInfoDto();

            if (dbModel != null)
            {
                dto.Id = dbModel.Id;
                dto.Name = dbModel.Name;
                dto.StatesSla = dbModel.StatesSla.ToSlaStateBasicInfoDto();
            }

            return dto;
        }

        public static SlaStateBasicInfoDto ToSlaStateBasicInfoDto(this StatesSla dbModel)
        {
            var dto = new SlaStateBasicInfoDto();

            if (dbModel != null)
            {
                dto.Id = dbModel.Id;
                dto.MinutesToTheEnd = dbModel.MinutesToTheEnd;
                dto.RowColor = dbModel.RowColor;
            }

            return dto;
        }

        public static IEnumerable<SlaStateBasicInfoDto> ToSlaStateBasicInfoDto(this IEnumerable<StatesSla> dbModelList)
        {
            var dto = new List<SlaStateBasicInfoDto>();

            if (dbModelList != null)
            {
                foreach (var dbModel in dbModelList)
                {
                    dto.Add(dbModel.ToSlaStateBasicInfoDto());
                }
            }

            return dto;
        }
    }
}
