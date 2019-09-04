using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.SalesRate;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.SalesRate;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SalesRateViewModelExtensions
    {
       public static IList<SalesRateViewModel> ToViewModel(this IList<SalesRateBaseDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static SalesRateBaseDto ToDto(this SalesRateViewModel source)
        {
            SalesRateBaseDto result = null;
            if (source != null)
            {
                result = new SalesRateBaseDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ReferenceERP = source.ReferenceERP
                };
            }
            return result;
        }

        public static ResultViewModel<SalesRateViewModel> ToViewModel(this ResultDto<SalesRateBaseDto> source)
        {
            var response = source != null ? new ResultViewModel<SalesRateViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static SalesRateViewModel ToViewModel(this SalesRateBaseDto source)
        {
            SalesRateViewModel result = null;
            if (source != null)
            {
                result = new SalesRateViewModel
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ReferenceERP = source.ReferenceERP
                };
            }
            return result;
        }
    }
}
