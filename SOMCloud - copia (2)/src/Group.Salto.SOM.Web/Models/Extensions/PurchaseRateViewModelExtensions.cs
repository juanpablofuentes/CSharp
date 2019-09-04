using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.PurchaseRate;
using Group.Salto.SOM.Web.Models.PurchaseRate;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PurchaseRateViewModelExtensions
    {
        public static ResultViewModel<PurchaseRateViewModel> ToViewModel(this ResultDto<PurchaseRateDto> source)
        {
            var response = source != null ? new ResultViewModel<PurchaseRateViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static PurchaseRateViewModel ToViewModel(this PurchaseRateDto source)
        {
            PurchaseRateViewModel result = null;
            if (source != null)
            {
                result = new PurchaseRateViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ReferenceErp = source.RefenrenceErp
                };
            }
            return result;
        }

        public static IList<PurchaseRateViewModel> ToViewModel(this IList<PurchaseRateDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static PurchaseRateDetailsDto ToDto(this PurchaseRateViewModel source)
        {
            PurchaseRateDetailsDto result = null;
            if (source != null)
            {
                result = new PurchaseRateDetailsDto()
                {
                    Id = source.Id ?? 0,
                    Name = source.Name,
                    Description = source.Description,
                    RefenrenceErp = source.ReferenceErp
                };
            }
            return result;
        }
    }
}
