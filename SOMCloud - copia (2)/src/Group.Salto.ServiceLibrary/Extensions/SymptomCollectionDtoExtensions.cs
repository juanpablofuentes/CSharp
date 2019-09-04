using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.SymptomCollection;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SymptomCollectionDtoExtensions
    {
        public static SymptomCollectionDto ToDto(this SymptomCollections source)
        {
            SymptomCollectionDto result = null;
            if (source != null)
            {
                result = new SymptomCollectionDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Element = source.Element
                };
                result.SymptomSelected = source.SymptomCollectionSymptoms?.Select(x => x.SymptomId)?.ToList();
            }
            return result;
        }

        public static SymptomCollections Update(this SymptomCollections target, SymptomCollectionDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Element = source.Element;
            }
            return target;
        }
    }
}