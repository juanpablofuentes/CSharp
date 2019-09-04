using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Journey;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Journey
{
    public interface IJourneyService
    {
        JourneyDto GetCurrentActiveJourney(int peopleId);
        ResultDto<JourneyDto> AddOrUpdate(int peopleConfigId, JourneyDto journeyDto);
    }
}
