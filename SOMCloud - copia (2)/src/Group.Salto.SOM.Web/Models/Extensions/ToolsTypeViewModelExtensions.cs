using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.ToolsType;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.ToolsType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ToolsTypeViewModelExtensions
    {
        public static ResultViewModel<ToolTypeViewModel> ToViewModel(this ResultDto<ToolsTypeDto> source)
        {
            var response = source != null ? new ResultViewModel<ToolTypeViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ToolTypeViewModel ToViewModel(this ToolsTypeDto source)
        {
            ToolTypeViewModel result = null;
            if (source != null)
            {
                result = new ToolTypeViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observation = source.Observations,
                };
            }
            return result;
        }

        public static IList<ToolTypeViewModel> ToViewModel(this IList<ToolsTypeDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ToolsTypeDto ToDto(this ToolTypeViewModel source)
        {
            ToolsTypeDto result = null;
            if (source != null)
            {
                result = new ToolsTypeDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observation,
                    
                    
                };
            }
            return result;
        }
    }
}