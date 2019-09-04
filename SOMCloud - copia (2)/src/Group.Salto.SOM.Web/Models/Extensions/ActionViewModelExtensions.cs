using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Actions;
using Group.Salto.SOM.Web.Models.Actions;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ActionViewModelExtensions
    {
        public static ResultViewModel<ActionViewModel> ToViewModel(this ResultDto<ActionDto> source)
        {
            var response = source != null ? new ResultViewModel<ActionViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ActionViewModel ToViewModel(this ActionDto source)
        {
            ActionViewModel result = null;
            if (source != null)
            {
                result = new ActionViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    UpdateDate = source.UpdateDate,
                    Description = source.Description
                };
            }
            return result;
        }

        public static ActionDto ToDto(this ActionViewModel source)
        {
            ActionDto result = null;
            if (source != null)
            {
                result = new ActionDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    UpdateDate = source.UpdateDate,
                    Description = source.Description
                };
            }
            return result;
        }

        public static IList<ActionViewModel> ToViewModel(this IList<ActionDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}