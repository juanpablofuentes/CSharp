using System.Collections.Generic;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedDay;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PredefinedDayDtoExtensions
    {
        public static PredefinedDayDto ToDto(this PredefinedDayStates dbModel)
        {
            var dto = new PredefinedDayDto
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Description = dbModel.Description,
                PredefinedType = (PredefinedDayEnum) dbModel.Id,
                PredefinedReasons = dbModel.PredefinedReasons.ToDto()
            };

            return dto;
        }

        public static IEnumerable<PredefinedDayDto> ToDto(this IEnumerable<PredefinedDayStates> dbModelList)
        {
            var dtoList = new List<PredefinedDayDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }

        public static PredefinedReasonDto ToDto(this PredefinedReasons dbModel)
        {
            var dto = new PredefinedReasonDto
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
            };

            return dto;
        }

        public static IEnumerable<PredefinedReasonDto> ToDto(this IEnumerable<PredefinedReasons> dbModelList)
        {
            var dtoList = new List<PredefinedReasonDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }
    }
}
