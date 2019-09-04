using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCollection;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PeopleCollectionDtoExtensions
    {
        public static PeopleCollectionDto ToDto(this PeopleCollections source)
        {
            PeopleCollectionDto result = null;
            if (source != null)
            {
                result = new PeopleCollectionDto();
                source.ToBaseDto(result);
                result.People = source.PeopleCollectionsPeople?.Select(x=> new PeopleSelectableDto()
                {
                    Id = x.People.Id,
                    Name = x.People.Name,
                    FirstSurname = x.People.FisrtSurname
                }).ToList();
                result.PeopleAdmin = source.PeopleCollectionsAdmins?.Select(x => new PeopleSelectableDto()
                {
                    Id = x.People.Id,
                    Name = x.People.Name,
                    FirstSurname = x.People.FisrtSurname
                }).ToList();
            }

            return result;
        }

        public static IList<PeopleCollectionDto> ToBaseDto(this IList<PeopleCollections> source)
        {
            return source?.MapList(s => s.ToDto());
        }

        public static PeopleCollections ToEntity(this PeopleCollectionDto source)
        {
            PeopleCollections result = null;
            if (source != null)
            {
                result = new PeopleCollections()
                {
                    Info = source.Description,
                    Name = source.Name,
                };
            }

            return result;
        } 
    }
}