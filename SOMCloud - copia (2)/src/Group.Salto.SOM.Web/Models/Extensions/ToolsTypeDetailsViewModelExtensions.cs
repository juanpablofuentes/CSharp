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
    public static class ToolsTypeDetailsViewModelExtensions
    {
        public static ResultViewModel<ToolsTypeDetailsViewModel> ToViewModel(this ResultDto<ToolsTypeDetailsDto> source)
        {
            var response = source != null ? new ResultViewModel<ToolsTypeDetailsViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ToolsTypeDetailsViewModel ToViewModel(this ToolsTypeDetailsDto source)
        {
            ToolsTypeDetailsViewModel result = null;
            if (source != null)
            {
                result = new ToolsTypeDetailsViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observation = source.Observations,
                    KnowledgeSelected = source.KnowledgeSelected.ToMultiComboViewModel()
                };
            }
            return result;
        }

        public static ToolsTypeDetailsDto ToDto(this ToolsTypeDetailsViewModel source)
        {
            ToolsTypeDetailsDto result = null;
            if (source != null)
            {
                result = new ToolsTypeDetailsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observation,
                    KnowledgeSelected = source.KnowledgeSelected.ToToolsTypeKnowledgeDto()
                };
            }
            return result;
        }
    }
}