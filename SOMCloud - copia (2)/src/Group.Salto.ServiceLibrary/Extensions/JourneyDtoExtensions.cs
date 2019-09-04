using System;
using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Journey;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedDay;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class JourneyDtoExtensions
    {
        public static JourneyDto ToDto(this Journeys dbModel)
        {
            var dto = new JourneyDto
            {
                Id = dbModel.Id,
                StartDate = dbModel.StartDate,
                Observations = dbModel.Observations,
                Finished = dbModel.Finished,
                CompanyVehicleId = dbModel.CompanyVehicleId,
                EndDate = dbModel.EndDate,
                StartKm = dbModel.StartKm,
                EndKm = dbModel.EndKm,
                JourneyStates = dbModel.JourneysStates.ToDto()
            };

            if (dto.StartKm >= 0)
            {
                dto.VehicleType = JourneyVehicleTypeEnum.OwnVehicle;
            }
            if (dto.CompanyVehicleId != null)
            {
                dto.VehicleType = JourneyVehicleTypeEnum.CompanyVehicle;
            }

            return dto;
        }

        public static IEnumerable<JourneyDto> ToDto(this IEnumerable<Journeys> dbModelList)
        {
            var dtoList = new List<JourneyDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }

        public static JourneyStateDto ToDto(this JourneysStates dbModel)
        {
            var dto = new JourneyStateDto
            {
                Id = dbModel.Id,
                Date = dbModel.Data,
                Latitude = dbModel.Latitude,
                Longitude = dbModel.Longitude,
                Observations = dbModel.Observations,
                PredefinedDayStatesId = dbModel.PredefinedDayStatesId,
                PredefinedReasonsId = dbModel.PredefinedReasonsId
            };

            if (dto.PredefinedDayStatesId <= 4)
            {
                dto.JourneyStateType = (PredefinedDayEnum) dto.PredefinedDayStatesId;
            }

            return dto;
        }

        public static IEnumerable<JourneyStateDto> ToDto(this IEnumerable<JourneysStates> dbModelList)
        {
            var dtoList = new List<JourneyStateDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }
    }
}
