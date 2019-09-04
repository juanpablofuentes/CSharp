using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Sites;
using Group.Salto.SOM.Web.Models.Sites;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SitesFilterViewModelExtensions
    {
        public static SitesFilterDto ToFilterDto(this SitesFilterViewModel source)
        {
            SitesFilterDto result = null;
            if (source != null)
            {
                result = new SitesFilterDto()
                {
                    finalClientId = source.FinalClientId,
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }

        public static SitesViewModel ToViewModel(this SitesListDto source)
        {
            SitesViewModel result = null;
            if (source != null)
            {
                result = new SitesViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this SitesListDto source, SitesViewModel target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Code = source.Code;
                target.City = source.City;
                target.Observations = source.Observations;
                target.IdSite = source.Id;
            }
        }

        public static IList<SitesViewModel> ToListViewModel(this IList<SitesListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}