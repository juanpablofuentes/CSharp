using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteUser;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SiteUserDetailDtoExtensions
    {
        public static SiteUserDetailDto ToDetailDto(this SiteUser source)
        {
            SiteUserDetailDto result = null;
            if (source != null)
            {
                result = new SiteUserDetailDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    FirstSurname = source.FirstSurname,
                    SecondSurname = source.SecondSurname,
                    Email = source.Email,
                    Telephone = source.Telephone,
                    Position = source.Position,
                    LocationId = source.LocationId,
                };
            }
            return result;
        }

        public static SiteUser ToEntity(this SiteUserDetailDto source)
        {
            SiteUser result = null;
            if (source != null)
            {
                result = new SiteUser();
                source.ToEntity(result);
            }

            return result;
        }

        public static void UpdateSiteUser (this SiteUser target, SiteUserDetailDto source)
        {
            target.Name = source.Name;
            target.FirstSurname = source.FirstSurname;
            target.SecondSurname = source.SecondSurname;
            target.Email = source.Email;
            target.Telephone = source.Telephone;
            target.Position = source.Position;
        }
    }
}