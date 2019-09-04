using Group.Salto.Common;
using Group.Salto.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IEventCategoriesRepository : IRepository<CalendarEventCategories>
    {
        IQueryable<CalendarEventCategories> GetAllNotDeleted();
        CalendarEventCategories GetByIdNotDeleted(int id);
        SaveResult<CalendarEventCategories> CreateEventCategories(CalendarEventCategories eventCategories);
        SaveResult<CalendarEventCategories> UpdateEventCategories(CalendarEventCategories eventCategories);
        bool DeleteEventCategories(CalendarEventCategories eventCategories);
        Dictionary<int, string> GetAllKeyValuesNotDeleted();
    }
}