using Group.Salto.Common.Helpers;
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
    public static class SlaViewModelExtensions
    {
        public static ResultViewModel<SlaViewModel> ToViewModel(this ResultDto<SlaDto> source)
        {
            var response = source != null ? new ResultViewModel<SlaViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static SlaViewModel ToViewModel(this SlaDto source)
        {
            SlaViewModel result = null;
            if (source != null)
            {
                result = new SlaViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    MinutesResponse = source.MinutesResponse,
                    MinutesResolutions = source.MinutesResolutions,
                    MinutesUnansweredPenalty = source.MinutesUnansweredPenalty,
                    MinutesPenaltyWithoutResolution = source.MinutesPenaltyWithoutResolution
                };
            }
            return result;
        }

        public static IList<SlaViewModel> ToViewModel(this IList<SlaDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}