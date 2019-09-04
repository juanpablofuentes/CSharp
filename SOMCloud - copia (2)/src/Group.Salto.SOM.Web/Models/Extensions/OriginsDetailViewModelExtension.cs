using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Origins;
using Group.Salto.SOM.Web.Models.Origins;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class OriginsDetailViewModelExtension
    {
        public static ResultViewModel<OriginDetailViewModel> ToDetailViewModel(this ResultDto<OriginsDto> source)
        {
            var response = source != null ? new ResultViewModel<OriginDetailViewModel>()
            {
                Data = source.Data.ToEditViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static OriginDetailViewModel ToEditViewModel(this OriginsDto source)
        {
            OriginDetailViewModel eventCategories = null;
            if (source != null)
            {
                eventCategories = new OriginDetailViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations
                };
            }
            return eventCategories;
        }

        public static OriginsDto ToDto(this OriginDetailViewModel source)
        {
            OriginsDto result = null;
            if (source != null)
            {
                result = new OriginsDto()
                {
                    Id = source.Id ?? 0,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations
                };
            }
            return result;
        }
    }
}