using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Tools;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ToolsDetailsViewModelExtensions
    {
        public static ResultViewModel<ToolsDetailsViewModel> ToDetailsViewModel(this ResultDto<ToolsDetailsDto> source)
        {
            var response = source != null ? new ResultViewModel<ToolsDetailsViewModel>()
            {
                Data = source.Data.ToDetailsViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ToolsDetailsViewModel ToDetailsViewModel(this ToolsDetailsDto source)
        {
            ToolsDetailsViewModel result = null;
            if (source != null)
            {
                result = new ToolsDetailsViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observation = source.Observations,
                    VehicleId = source.VehicleId,
                    VehicleName = source.VehicleName, 
                    Types = source.Types.ToMultiComboViewModel()
                };
            }
            return result;
        }

        public static ToolsDetailsDto ToDto(this ToolsDetailsViewModel source)
        {
            ToolsDetailsDto result = null;
            if (source != null)
            {
                result = new ToolsDetailsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observation,
                    VehicleId = source.VehicleId,
                    VehicleName = source.VehicleName,
                    Types = source.Types.ToToolsToolTypeDto()
                };
            }
            return result;
        }
    }
}