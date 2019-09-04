using Group.Salto.ServiceLibrary.Common.Dtos.SymptomCollection;
using Group.Salto.SOM.Web.Models.SymptomCollection;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SymptomCollectionFilterViewModelExtensions
    {
        public static SymptomCollectionFilterDto ToDto(this SymptomCollectionFilterViewModel source)
        {
            SymptomCollectionFilterDto result = null;
            if (source != null)
            {
                result = new SymptomCollectionFilterDto()
                {
                    Name = source.Name,
                    Element = source.Element,                    
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}