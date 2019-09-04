using Group.Salto.ServiceLibrary.Common.Dtos.SiteUser;
using Group.Salto.SOM.Web.Models.SiteUser;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SiteUserViewModelExtensions
    {
        public static void ToDto(this SiteUserDetailViewModel source, SiteUserDetailDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.IdUser;
                target.Name = source.Name;
                target.FirstSurname = source.FirstSurname;
                target.SecondSurname = source.SecondSurname;
                target.Email = source.Email;
                target.Telephone = source.Telephone;
                target.Position = source.Position;
            }
        }
    }
}