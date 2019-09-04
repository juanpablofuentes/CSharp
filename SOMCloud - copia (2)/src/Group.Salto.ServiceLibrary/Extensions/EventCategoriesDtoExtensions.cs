using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.EventCategories;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class EventCategoriesDtoExtensions
    {
        public static EventCategoriesDto ToListDto(this CalendarEventCategories source)
        {
            EventCategoriesDto result = null;
            if (source != null)
            {
                result = new EventCategoriesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    AvailabilityCategoriesId = source.AvailabilityCategoriesId
                };
            }
            return result;
        }

        public static IList<EventCategoriesDto> ToDto(this IList<CalendarEventCategories> source)
        {
            return source.MapList(x => x.ToListDto());
        }

        public static CalendarEventCategories ToEntity(this EventCategoriesDto source)
        {
            CalendarEventCategories result = null;
            if (source != null)
            {
                result = new CalendarEventCategories()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    AvailabilityCategoriesId= source.AvailabilityCategoriesId
                };
            }

            return result;
        }

        public static CalendarEventCategories Update(this CalendarEventCategories target, EventCategoriesDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.AvailabilityCategoriesId = source.AvailabilityCategoriesId;
            }

            return target;
        }
    }
}
