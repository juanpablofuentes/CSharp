using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Symptoms;
using Group.Salto.SOM.Web.Models.Symptom;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SymptomChildViewModelExtensions
    {
        public static SymptomChildViewModel ToViewModel(this SymptomChildDto source)
        {
            SymptomChildViewModel result = null;
            if (source != null)
            {
                result = new SymptomChildViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description
                };                
                result.Childs = source.Symptoms.ToViewModel();
            }
            return result;
        }

        public static IList<SymptomChildViewModel> ToViewModel(this IList<SymptomChildDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}