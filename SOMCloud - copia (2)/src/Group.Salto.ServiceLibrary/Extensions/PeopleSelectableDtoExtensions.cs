using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.People;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PeopleSelectableDtoExtensions
    {
        public static PeopleSelectableDto ToPeopleSelectableDto(this People source)
        {
            PeopleSelectableDto result = null;
            if (source != null)
            {
                result = new PeopleSelectableDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    FirstSurname = source.FisrtSurname,
                    IsResponsable = source.SubcontractorResponsible ?? false,
                };
            }
            return result;
        }

        public static IList<PeopleSelectableDto> ToPeopleSelectableDto(this IList<People> source)
        {
            return source?.MapList(x => x.ToPeopleSelectableDto());
        }
    }
}