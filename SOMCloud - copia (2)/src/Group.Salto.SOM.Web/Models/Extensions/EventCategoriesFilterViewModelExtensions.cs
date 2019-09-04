using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.EventCategories;
using Group.Salto.SOM.Web.Models.EventCategories;
using Group.Salto.SOM.Web.Models.EventCategoriesViewModel;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class EventCategoriesFilterViewModelExtension
    {
        public static EventCategoriesFilterDto ToFilterDto(this EventCategoriesFilterViewModel source)
        {
            EventCategoriesFilterDto result = null;
            if (source != null)
            {
                result = new EventCategoriesFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }

        public static EventCategoriesListViewModel ToViewModel(this EventCategoriesDto source)
        {
            EventCategoriesListViewModel eventCategories = null;
            if (source != null)
            {
                eventCategories = new EventCategoriesListViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                };
            }
            return eventCategories;
        }

        public static IList<EventCategoriesListViewModel> ToViewModel(this IList<EventCategoriesDto> source)
        {
            return source?.MapList(ec => ec.ToViewModel());
        }

    }
}
