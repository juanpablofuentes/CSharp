using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.PointRate;
using Group.Salto.SOM.Web.Models.PointRate;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PointsRateViewModelExtensions
    {
        public static ResultViewModel<PointRateViewModel> ToViewModel(this ResultDto<PointRateDto> source)
        {
            var response = source != null ? new ResultViewModel<PointRateViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static PointRateViewModel ToViewModel(this PointRateDto source)
        {
            PointRateViewModel result = null;
            if (source != null)
            {
                result = new PointRateViewModel()
                {
                    Id =source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ErpReference = source.ErpReference
                };
            }
            return result;
        }

        public static IList<PointRateViewModel> ToViewModel(this IList<PointRateDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static PointRateDto ToDto(this PointRateViewModel source)
        {
            PointRateDto result = null;
            if (source != null)
            {
                result = new PointRateDto()
                {
                    Id = source.Id ?? 0,
                    Name = source.Name,
                    Description = source.Description,
                    ErpReference = source.ErpReference
                };
            }
            return result;
        }
    }
}
