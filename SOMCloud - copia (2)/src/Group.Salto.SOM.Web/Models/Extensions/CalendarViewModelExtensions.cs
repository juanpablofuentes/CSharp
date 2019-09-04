using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.SOM.Web.Models.Calendar;
using Group.Salto.SOM.Web.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CalendarViewModelExtensions
    {
        public static CalendarViewModel ToViewModel(this CalendarDto source)
        {
            CalendarViewModel result = null;
            if (source != null)
            {
                result = new CalendarViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Color = source.Color,
                    IsPrivate = source.IsPrivate,
                    Priority = source.Priority,
                    Events = source.Events.ToViewModel()
                };
            }

            return result;
        }

        public static IList<CalendarViewModel> ToViewModel(this IList<CalendarDto> source)
        {
            return source?.MapList(pc => pc.ToViewModel());
        }
    }
}