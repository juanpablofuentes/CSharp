using Group.Salto.ServiceLibrary.Common.Dtos.Symptoms;
using Group.Salto.SOM.Web.Models.Symptom;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SymptomDetailViewModelExtensions
    {
        public static SymptomDetailViewModel ToDetailViewModel(this SymptomBaseDto source)
        {
            SymptomDetailViewModel result = null;
            if (source != null)
            {
                result = new SymptomDetailViewModel()
                {
                     Symptom = source.ToViewModel()
                };                
            }
            return result;
        }
    }
}