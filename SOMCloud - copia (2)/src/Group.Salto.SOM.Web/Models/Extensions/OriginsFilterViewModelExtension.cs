using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Origins;
using Group.Salto.SOM.Web.Models.Origins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class OriginsFilterViewModelExtension
    {
        public static OriginsFilterDto ToFilterDto(this OriginsFilterViewModel source)
        {
            OriginsFilterDto result = null;
            if (source != null)
            {
                result = new OriginsFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }

        public static OriginListViewModel ToViewModel(this OriginsDto source)
        {
            OriginListViewModel eventCategories = null;
            if (source != null)
            {
                eventCategories = new OriginListViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations
                };
            }
            return eventCategories;
        }

        public static IList<OriginListViewModel> ToViewModel(this IList<OriginsDto> source)
        {
            return source?.MapList(ec => ec.ToViewModel());
        }
    }
}
