using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Knowledge;
using Group.Salto.SOM.Web.Models.Knowledge;
using Group.Salto.SOM.Web.Models.Result;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class KnowledgeViewModelExtension
    {
        public static ResultViewModel<KnowledgeViewModel> ToViewModel(this ResultDto<KnowledgeDto> source)
        {
            var response = source != null ? new ResultViewModel<KnowledgeViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static KnowledgeViewModel ToViewModel(this KnowledgeDto source)
        {
            KnowledgeViewModel result = null;
            if (source != null)
            {
                result = new KnowledgeViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    UpdateDate = source.UpdateDate,
                    Description = source.Description,
                    Observations = source.Observations
                };
            }
            return result;
        }

        public static IList<KnowledgeViewModel> ToViewModel(this IList<KnowledgeDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static KnowledgeDto ToDto(this KnowledgeViewModel source)
        {
            KnowledgeDto result = null;
            if (source != null)
            {
                result = new KnowledgeDto()
                {
                    Id = source.Id ?? 0,
                    Name = source.Name,
                    UpdateDate = source.UpdateDate,
                    Description = source.Description,
                    Observations = source.Observations
                };
            }
            return result;
        }
    }
}
