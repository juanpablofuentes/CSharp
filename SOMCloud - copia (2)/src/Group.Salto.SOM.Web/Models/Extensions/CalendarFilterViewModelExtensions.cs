using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.SOM.Web.Models.Calendar;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CalendarFilterViewModelExtensions
    {
        public static CalendarFilterDto ToDto(this CalendarFilterViewModel source)
        {
            CalendarFilterDto result = null;
            if (source != null)
            {
                result = new CalendarFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy
                };
            }

            return result;
        }
    }
}