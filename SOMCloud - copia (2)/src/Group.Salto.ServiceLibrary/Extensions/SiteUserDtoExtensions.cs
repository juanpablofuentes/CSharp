using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteUser;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SiteUserDtoExtensions
    {
        public static SiteUserListDto ToListDto(this SiteUser source)
        {
            SiteUserListDto result = null;
            if (source != null)
            {
                result = new SiteUserListDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    FirstSurname = source.FirstSurname,
                    SecondSurname = source.SecondSurname,
                    Email = source.Email,
                    Telephone = source.Telephone,
                };

            }
            return result;
        }

        public static IList<SiteUserListDto> ToListDto(this IList<SiteUser> source)
        {
            return source?.MapList(x => x.ToListDto());
        }

        public static void ToEntity(this SiteUserDetailDto source, SiteUser target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.SecondSurname = source.SecondSurname;
                target.FirstSurname = source.FirstSurname;
                target.Email = source.Email;
                target.Telephone = source.Telephone;
                target.Position = source.Position;
                target.LocationId = source.LocationId;
            }
        }
    }
}