using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleVisible;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PeopleVisibleListDtoExtensions
    {
        public static PeopleVisibleListDto ToVisibleListDto(this People source)
        {
            PeopleVisibleListDto result = null;
            if (source != null)
            {
                result = new PeopleVisibleListDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    FisrtSurname = source.FisrtSurname,
                    SecondSurname = source.SecondSurname,
                    Email = source.Email,
                    Telephone = source.Telephone,
                    Extension = source.Extension,
                    Company = source.Company?.Name,
                    Department = source.Department?.Name
                };
            }

            return result;
        }

        public static IEnumerable<PeopleVisibleListDto> ToVisibleListDto(this IEnumerable<People> source)
        {
            return source?.MapList(c => c.ToVisibleListDto());
        }
    }
}