using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteUser;
using Group.Salto.SOM.Web.Models.SiteUser;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SiteUserFilterViewModelExtensions
    {
        public static SiteUserFilterDto ToFilterDto(this SiteUserFilterViewModel source)
        {
            SiteUserFilterDto result = null;
            if (source != null)
            {
                result = new SiteUserFilterDto()
                {
                    SitesId = source.SitesId,
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }

        public static SiteUserViewModel ToViewModel(this SiteUserListDto source)
        {
            SiteUserViewModel result = null;
            if (source != null)
            {
                result = new SiteUserViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this SiteUserListDto source, SiteUserViewModel target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.FirstSurname = source.FirstSurname;
                target.SecondSurname = source.SecondSurname;
                target.Email = source.Email;
                target.Telephone = source.Telephone;
                target.IdUser = source.Id;
            }
        }

        public static IList<SiteUserViewModel> ToListViewModel(this IList<SiteUserListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}