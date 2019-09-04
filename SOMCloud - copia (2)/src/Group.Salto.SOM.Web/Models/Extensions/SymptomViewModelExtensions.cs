using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Symptoms;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Symptom;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SymptomViewModelExtensions
    {
        public static IList<SymptomViewModel> ToViewModel(this IList<SymptomBaseDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static SymptomViewModel ToViewModel(this SymptomBaseDto source)
        {
            SymptomViewModel result = null;
            if (source != null)
            {
                result = new SymptomViewModel()
                {
                     Id = source.Id,
                     Name = source.Name,
                     Description = source.Description
                };                
            }

            return result;
        }

        public static SymptomBaseDto ToDto(this SymptomViewModel source)
        {
            SymptomBaseDto result = null;
            if (source != null)
            {
                result = new SymptomBaseDto
                {
                    Id = source.Id ?? 0,
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }

        public static ResultViewModel<SymptomViewModel> ToViewModel(this ResultDto<SymptomBaseDto> source)
        {
            var response = source != null ? new ResultViewModel<SymptomViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }
    }
}