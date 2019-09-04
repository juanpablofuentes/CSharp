using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.SOM.Web.Models.Calendar;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CalendarBaseViewModelExtensions
    {
        public static ResultViewModel<CalendarBaseViewModel> ToResultViewModel(this ResultDto<CalendarBaseDto> source)
        {
            ResultViewModel<CalendarBaseViewModel> result = null;
            if (source != null)
            {
               result = new ResultViewModel<CalendarBaseViewModel>()
               {
                   Data = source.Data.ToViewModel(),
                   Feedbacks = source.Errors.ToViewModel(),
               };
            }

            return result;
        }

        public static CalendarBaseViewModel ToViewModel(this CalendarBaseDto source)
        {
            CalendarBaseViewModel result = null;
            if (source != null)
            {
                result = new CalendarBaseViewModel();
                source.ToBaseViewModel(result);
            }

            return result;
        }

        public static void ToBaseViewModel(this CalendarBaseDto source, CalendarBaseViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Color = source.Color;
                target.Description = source.Description;
                target.IsPrivate = source.IsPrivate;
                target.HasPeopleAssigned = source.HasPeopleAssigned;
            }
        }

        public static CalendarBaseDto ToDto(this CalendarBaseViewModel source)
        {
            CalendarBaseDto result = null;
            if (source != null)
            {
                result = new CalendarBaseDto();
                source.ToBaseDto(result);
            }

            return result;
        }

        public static void ToBaseDto(this CalendarBaseViewModel source, CalendarBaseDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Color = source.Color;
                target.Description = source.Description;
                target.IsPrivate = source.IsPrivate;
                target.HasPeopleAssigned = source.HasPeopleAssigned;
            }
        }

        public static IList<CalendarBaseViewModel> ToViewModel(this IList<CalendarBaseDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}