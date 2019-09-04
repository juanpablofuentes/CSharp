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
    public static class PurchaseRateDetailsModelExtensions
    {
        public static ResultViewModel<PurchaseRateDetailsViewModel> ToViewModel(this ResultDto<PurchaseRateDetailsDto> source)
        {
            var response = source != null ? new ResultViewModel<PurchaseRateDetailsViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static PurchaseRateDetailsViewModel ToViewModel(this PurchaseRateDetailsDto source)
        {
            PurchaseRateDetailsViewModel result = null;
            if (source != null)
            {
                result = new PurchaseRateDetailsViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ReferenceErp = source.RefenrenceErp
                };
            }
            return result;
        }

        public static PurchaseRateDetailsDto ToDto(this PurchaseRateDetailsViewModel source)
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