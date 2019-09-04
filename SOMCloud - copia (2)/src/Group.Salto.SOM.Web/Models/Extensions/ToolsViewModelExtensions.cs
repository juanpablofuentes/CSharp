using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Tools;
using Group.Salto.SOM.Web.Models.Tools;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ToolsViewModelExtensions
    {
        public static ResultViewModel<ToolViewModel> ToViewModel(this ResultDto<ToolsDto> source)
        {
            var response = source != null ? new ResultViewModel<ToolViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ToolViewModel ToViewModel(this ToolsDto source)
        {
            ToolViewModel result = null;
            if (source != null)
            {
                result = new ToolViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observation = source.Observations,
                    VehicleId = source.VehicleId,
                    VehicleName = source.VehicleName
                };
            }
            return result;
        }

        public static IList<ToolViewModel> ToViewModel(this IList<ToolsDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ToolsDto ToDto(this ToolViewModel source)
        {
            ToolsDto result = null;
            if (source != null)
            {
                result = new ToolsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observation,
                    VehicleId = source.VehicleId,
                    VehicleName = source.VehicleName
                };
            }
            return result;
        }
    }
}