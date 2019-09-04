using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.SymptomCollection;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SymptomCollectionBaseDtoExtensions
    {
        public static SymptomCollectionBaseDto ToBaseDto(this SymptomCollections source)
        {
            SymptomCollectionBaseDto result = null;
            if (source != null)
            {
                result = new SymptomCollectionBaseDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Element = source.Element,                     
                    Description = source.Description
                };
            }
            return result;
        }

        public static IList<SymptomCollectionBaseDto> ToBaseDto(this IList<SymptomCollections> source)
        {
            return source?.MapList(c => c.ToBaseDto());
        }

        public static SymptomCollections ToEntity(this SymptomCollectionDto source)
        {
            SymptomCollections result = null;
            if (source != null)
            {
                result = new SymptomCollections
                {
                    Id = source.Id,
                    Name = source.Name,
                    Element = source.Element,
                    Description = source.Description,
                };                
            }
            return result;
        }
    }
}