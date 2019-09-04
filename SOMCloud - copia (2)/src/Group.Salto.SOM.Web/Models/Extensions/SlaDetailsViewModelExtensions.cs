using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Sla;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Sla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SlaDetailsViewModelExtensions
    {
        public static ResultViewModel<SlaDetailsViewModel> ToViewModel(this ResultDto<SlaDetailsDto> source)
        {
            var response = source != null ? new ResultViewModel<SlaDetailsViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static SlaDetailsViewModel ToViewModel(this SlaDetailsDto source)
        {
            SlaDetailsViewModel result = null;
            if (source != null)
            {
                result = new SlaDetailsViewModel()
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
                    ReferenceMinutesResponse = source.ReferenceMinutesResponse,
                    ReferenceMinutesResolution = source.ReferenceMinutesResolution,
                    ReferenceMinutesPenaltyUnanswered = source.ReferenceMinutesPenaltyUnanswered,
                    ReferenceMinutesPenaltyWithoutResolution = source.ReferenceMinutesPenaltyWithoutResolution,
                    ReferenceTime = source.ReferenceTime,
                    StatesSla = source.StatesSla.ToMultiComboViewModelSla()
                };
            }
            return result;
        }

        public static SlaDetailsDto ToDto(this SlaDetailsViewModel source)
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
                    ReferenceTime = source.ReferenceTime,
                    ReferenceMinutesPenaltyUnanswered = source.ReferenceMinutesPenaltyUnanswered,
                    ReferenceMinutesPenaltyWithoutResolution = source.ReferenceMinutesPenaltyWithoutResolution,
                    ReferenceMinutesResolution = source.ReferenceMinutesResolution,
                    ReferenceMinutesResponse = source.ReferenceMinutesResponse,
                    StatesSla = source.StatesSla.ToStatesSlaDto()
                };
            }
            return result;
        }
    }
}