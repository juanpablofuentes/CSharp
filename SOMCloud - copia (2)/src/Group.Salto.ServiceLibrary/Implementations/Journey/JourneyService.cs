using System;
using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Journey;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Journey;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedDay;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.Journey
{
    public class JourneyService : BaseService, IJourneyService
    {
        private readonly IJourneyRepository _journeyRepository;
        private readonly IPeopleRepository _peopleRepository;

        public JourneyService(IJourneyRepository journeyRepository,
                              IPeopleRepository peopleRepository,
            ILoggingService logginingService) : base(logginingService)
        {
            _journeyRepository = journeyRepository;
            _peopleRepository = peopleRepository;
        }

        public JourneyDto GetCurrentActiveJourney(int peopleConfigId)
        {
            var people = _peopleRepository.GetByConfigId(peopleConfigId);
            var currentJourney = _journeyRepository.GetCurrentActiveJourneyByPeopleId(people.Id);
            var dto = currentJourney?.ToDto();
            return dto;
        }

        public ResultDto<JourneyDto> AddOrUpdate(int peopleConfigId, JourneyDto journeyDto)
        {
            ResultDto<JourneyDto> resultDto = null;
            foreach (var journeyState in journeyDto.JourneyStates)
            {
                switch (journeyState.JourneyStateType)
                {
                    case PredefinedDayEnum.InitJourney:
                        resultDto = CreateJourney(peopleConfigId, journeyDto, journeyState);
                        break;
                    case PredefinedDayEnum.PauseJourney:
                    case PredefinedDayEnum.ContinueJourney:
                        resultDto = AddStateToJourney(journeyDto, journeyState);
                        break;
                    case PredefinedDayEnum.FinishJourney:
                        resultDto = FinishJourney(journeyDto, journeyState);
                        break;
                }
            }

            return resultDto;
        }

        private ResultDto<JourneyDto> FinishJourney(JourneyDto journeyDto, JourneyStateDto journeyState)
        {
            var currentJourney = _journeyRepository.GetByIdIncludeStates(journeyDto.Id);
            currentJourney.EndDate = journeyDto.EndDate;
            currentJourney.Finished = true;
            currentJourney.EndKm = journeyDto.EndKm;
            var result = ApplyStateToCurrentJourney(journeyState, currentJourney);
            return result;
        }

        private ResultDto<JourneyDto> AddStateToJourney(JourneyDto journeyDto, JourneyStateDto journeyState)
        {
            var currentJourney = _journeyRepository.GetByIdIncludeStates(journeyDto.Id);
            var result =  ApplyStateToCurrentJourney(journeyState, currentJourney);
            return result;
        }

        private ResultDto<JourneyDto> ApplyStateToCurrentJourney(JourneyStateDto journeyState, Journeys currentJourney)
        {
            currentJourney.JourneysStates.Add(new JourneysStates
            {
                UpdateDate = DateTime.UtcNow,
                Observations = journeyState.Observations,
                Latitude = journeyState.Latitude,
                Longitude = journeyState.Longitude,
                PredefinedDayStatesId = (int) journeyState.JourneyStateType,
                PredefinedReasonsId = journeyState.PredefinedReasonsId,
                Data = journeyState.Date
            });

            var resultRepository = _journeyRepository.UpdateJourney(currentJourney);
            var process = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            var result = new ResultDto<JourneyDto>
            {
                Data = process.Data,
                Errors = process.Errors
            };
            return result;
        }

        private ResultDto<JourneyDto> CreateJourney(int peopleConfigId, JourneyDto journeyDto, JourneyStateDto journeyState)
        {
            var people = _peopleRepository.GetByConfigId(peopleConfigId);

            var dbJourney = new Journeys
            {
                UpdateDate = DateTime.UtcNow,
                StartDate = journeyDto.StartDate,
                Observations = journeyState.Observations,
                People = people,
                JourneysStates = new List<JourneysStates>
                {
                    new JourneysStates
                    {
                        UpdateDate = DateTime.UtcNow,
                        Observations = journeyState.Observations,
                        Latitude = journeyState.Latitude,
                        Longitude = journeyState.Longitude,
                        PredefinedDayStatesId = (int)journeyState.JourneyStateType,
                        PredefinedReasonsId = journeyState.PredefinedReasonsId,
                        Data = journeyState.Date
                    }
                }
            };

            switch (journeyDto.VehicleType)
            {
                case JourneyVehicleTypeEnum.NoVehicle:
                    dbJourney.StartKm = -1;
                    break;
                case JourneyVehicleTypeEnum.CompanyVehicle:
                    dbJourney.StartKm = journeyDto.StartKm;
                    dbJourney.IsCompanyVehicle = true;
                    dbJourney.CompanyVehicleId = journeyDto.CompanyVehicleId;
                    break;
            }

            var resultRepository = _journeyRepository.CreateJourney(dbJourney);
            var process = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            var result = new ResultDto<JourneyDto>
            {
                Data = process.Data,
                Errors = process.Errors
            };
            journeyDto.Id = result.Data?.Id ?? 0;
            return result;
        }
    }
}
