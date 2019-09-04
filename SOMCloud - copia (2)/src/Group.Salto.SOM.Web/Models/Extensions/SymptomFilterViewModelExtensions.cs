using Group.Salto.ServiceLibrary.Common.Dtos.Symptoms;
using Group.Salto.SOM.Web.Models.Symptom;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SymptomFilterViewModelExtensions
    {
        public static SymptomFilterDto ToDto(this SymptomFilterViewModel source)
        {
            SymptomFilterDto result = null;
            if (source != null)
            {
                result = new SymptomFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}