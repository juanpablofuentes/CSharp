using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Sla;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SlaDetailsDtoExtensions
    {
        public static SlaDetailsDto ToDetailDto(this Sla source)
        {
            SlaDetailsDto result = null;
            if (source != null)
            {
                result = new SlaDetailsDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    MinutesResponse = source.MinutesResponse,
                    MinutesNaturalResponse = source.MinutesNaturalResponse,
                    MinutesResolutions = source.MinutesResolutions,
                    MinutesResolutionNaturals = source.MinutesResolutionNaturals,
                    MinutesUnansweredPenalty = source.MinutesUnansweredPenalty,
                    MinutesWithoutNaturalResponse = source.MinutesWithoutNaturalResponse,
                    MinutesPenaltyWithoutResponse = source.MinutesPenaltyWithoutResponse,
                    MinutesPenaltyWithoutResponseNaturals = source.MinutesPenaltyWithoutResponseNaturals,
                    MinutesPenaltyWithoutResolution = source.MinutesPenaltyWithoutResolution,
                    MinutesPenaltyWithoutNaturalResolution = source.MinutesPenaltyWithoutNaturalResolution,
                    TimeResponseActive = source.TimeResponseActive,
                    TimeResolutionActive = source.TimeResolutionActive,
                    TimePenaltyWithoutResponseActive = source.TimePenaltyWithoutResponseActive,
                    TimePenaltyWhithoutResolutionActive = source.TimePenaltyWhithoutResolutionActive,
                    MinutesResponseOtDefined = source.MinutesResponseOtDefined,
                    MinutesResolutionOtDefined = source.MinutesResolutionOtDefined,
                    MinutesPenaltyWithoutResponseOtDefined = source.MinutesPenaltyWithoutResponseOtDefined,
                    MinutesPenaltyWithoutResolutionOtDefined = source.MinutesPenaltyWithoutResolutionOtDefined,
                    ReferenceMinutesPenaltyUnanswered = source.ReferenceMinutesPenaltyUnanswered,
                    ReferenceMinutesPenaltyWithoutResolution = source.ReferenceMinutesPenaltyWithoutResolution,
                    ReferenceMinutesResolution = source.ReferenceMinutesResolution,
                    ReferenceMinutesResponse = source.ReferenceMinutesResponse,
                    StatesSla = source.StatesSla?.ToList().ToStatesSlaDto()
                };
            }
            return result;
        }

        public static Sla ToEntity(this SlaDetailsDto source)
        {
            Sla result = null;
            if (source != null)
            {
                result = new Sla()
                {
                    Name = source.Name,
                    Id = source.Id,
                    MinutesResponse = source.MinutesResponse,
                    MinutesNaturalResponse = source.MinutesNaturalResponse,
                    MinutesResolutions = source.MinutesResolutions,
                    MinutesResolutionNaturals = source.MinutesResolutionNaturals,
                    MinutesUnansweredPenalty = source.MinutesUnansweredPenalty,
                    MinutesWithoutNaturalResponse = source.MinutesWithoutNaturalResponse,
                    MinutesPenaltyWithoutResponse = source.MinutesPenaltyWithoutResponse,
                    MinutesPenaltyWithoutResponseNaturals = source.MinutesPenaltyWithoutResponseNaturals,
                    MinutesPenaltyWithoutResolution = source.MinutesPenaltyWithoutResolution,
                    MinutesPenaltyWithoutNaturalResolution = source.MinutesPenaltyWithoutNaturalResolution,
                    TimeResponseActive = source.TimeResponseActive,
                    TimeResolutionActive = source.TimeResolutionActive,
                    TimePenaltyWithoutResponseActive = source.TimePenaltyWithoutResponseActive,
                    TimePenaltyWhithoutResolutionActive = source.TimePenaltyWhithoutResolutionActive,
                    MinutesResponseOtDefined = source.MinutesResponseOtDefined,
                    MinutesResolutionOtDefined = source.MinutesResolutionOtDefined,
                    MinutesPenaltyWithoutResponseOtDefined = source.MinutesPenaltyWithoutResponseOtDefined,
                    MinutesPenaltyWithoutResolutionOtDefined = source.MinutesPenaltyWithoutResolutionOtDefined,
                    ReferenceMinutesPenaltyUnanswered = source.ReferenceMinutesPenaltyUnanswered,
                    ReferenceMinutesPenaltyWithoutResolution = source.ReferenceMinutesPenaltyWithoutResolution,
                    ReferenceMinutesResolution = source.ReferenceMinutesResolution,
                    ReferenceMinutesResponse = source.ReferenceMinutesResponse,
                    StatesSla = source.StatesSla.ToEntitySla()
                };
            }
            return result;
        }

        public static Sla Update(this Sla target, SlaDetailsDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.MinutesResponse = source.MinutesResponse;
                target.MinutesNaturalResponse = source.MinutesNaturalResponse;
                target.MinutesResolutions = source.MinutesResolutions;
                target.MinutesResolutionNaturals = source.MinutesResolutionNaturals;
                target.MinutesUnansweredPenalty = source.MinutesUnansweredPenalty;
                target.MinutesWithoutNaturalResponse = source.MinutesWithoutNaturalResponse;
                target.MinutesPenaltyWithoutResponse = source.MinutesPenaltyWithoutResponse;
                target.MinutesPenaltyWithoutResponseNaturals = source.MinutesPenaltyWithoutResponseNaturals;
                target.MinutesPenaltyWithoutResolution = source.MinutesPenaltyWithoutResolution;
                target.MinutesPenaltyWithoutNaturalResolution = source.MinutesPenaltyWithoutNaturalResolution;
                target.ReferenceMinutesResponse = source.ReferenceMinutesResponse;
                target.ReferenceMinutesResolution = source.ReferenceMinutesResolution;
                target.ReferenceMinutesPenaltyUnanswered = source.ReferenceMinutesPenaltyUnanswered;
                target.ReferenceMinutesPenaltyWithoutResolution = source.ReferenceMinutesPenaltyWithoutResolution;
                target.TimeResponseActive = source.TimeResponseActive;
                target.TimeResolutionActive = source.TimeResolutionActive;
                target.TimePenaltyWithoutResponseActive = source.TimePenaltyWithoutResponseActive;
                target.TimePenaltyWhithoutResolutionActive = source.TimePenaltyWhithoutResolutionActive;
                target.MinutesResponseOtDefined = source.MinutesResponseOtDefined;
                target.MinutesResolutionOtDefined = source.MinutesResolutionOtDefined;
                target.MinutesPenaltyWithoutResponseOtDefined = source.MinutesPenaltyWithoutResponseOtDefined;
                target.MinutesPenaltyWithoutResolutionOtDefined = source.MinutesPenaltyWithoutResolutionOtDefined;
            }

            return target;
        }
    }
}