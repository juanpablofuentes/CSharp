using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.SymptomCollection;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SymptomCollectionSymptomsDtoExtensions
    {
        public static SymptomCollectionSymptomsDto ToDto(this SymptomCollectionSymptoms source)
        {
            SymptomCollectionSymptomsDto result = null;
            if (source != null)
            {
                result = new SymptomCollectionSymptomsDto()
                {
                    SymptomCollectionId = source.SymptomCollectionId,
                    SymptomId = source.SymptomId
                };
            }
            return result;
        }

        public static IList<SymptomCollectionSymptomsDto> ToDto(this IList<SymptomCollectionSymptoms> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}