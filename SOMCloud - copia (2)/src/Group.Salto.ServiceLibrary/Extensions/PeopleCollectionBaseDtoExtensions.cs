using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCollection;
using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PeopleCollectionBaseDtoExtensions
    {
        public static PeopleCollectionBaseDto ToBaseDto(this PeopleCollections source)
        {
            PeopleCollectionBaseDto result = null;
            if (source != null)
            {
                result = new PeopleCollectionBaseDto();
                source.ToBaseDto(result);
            }

            return result;
        }

        public static void ToBaseDto(this PeopleCollections source, PeopleCollectionBaseDto target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Description = source.Info;
                target.Id = source.Id;
            }
        }

        public static IList<PeopleCollectionBaseDto> ToBaseDto(this IList<PeopleCollections> source)
        {
            return source?.MapList(s => s.ToBaseDto());
        }
    }
}