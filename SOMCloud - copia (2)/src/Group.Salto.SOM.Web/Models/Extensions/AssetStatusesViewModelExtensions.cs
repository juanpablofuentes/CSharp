using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.AssetStatuses;
using Group.Salto.SOM.Web.Models.AssetStatuses;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class AssetStatusesViewModelExtensions
    {
        public static ResultViewModel<AssetStatusesViewModel> ToViewModel(this ResultDto<AssetStatusesDto> source)
        {
            var response = source != null ? new ResultViewModel<AssetStatusesViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static AssetStatusesViewModel ToViewModel(this AssetStatusesDto source)
        {
            AssetStatusesViewModel result = null;
            if (source != null)
            {
                result = new AssetStatusesViewModel()
                {
                    Id = source.Id,
                    Color = source.Color,
                    Name = source.Name,
                    IsDefault= source.IsDefault,
                    IsRetiredState = source.IsRetiredState
                };
            }
            return result;
        }

        public static IList<AssetStatusesViewModel> ToViewModel(this IList<AssetStatusesDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static AssetStatusesDto ToDto(this AssetStatusesViewModel source)
        {
            AssetStatusesDto result = null;
            if (source != null)
            {
                result = new AssetStatusesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Color = source.Color,
                    IsDefault = source.IsDefault,
                    IsRetiredState = source.IsRetiredState
                };
            }
            return result;
        }
    }
}