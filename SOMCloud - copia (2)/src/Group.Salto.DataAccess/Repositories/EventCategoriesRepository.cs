using DataAccess.Common;
using Group.Salto.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class EventCategoriesRepository : BaseRepository<CalendarEventCategories>, IEventCategoriesRepository
    {
        public EventCategoriesRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public SaveResult<CalendarEventCategories> CreateEventCategories(CalendarEventCategories eventCategories)
        {
            eventCategories.UpdateDate = DateTime.UtcNow;
            Create(eventCategories);
            var result = SaveChange(eventCategories);
            return result;
        }

        public bool DeleteEventCategories(CalendarEventCategories eventCategories)
        {
            eventCategories.UpdateDate = DateTime.UtcNow;
            Delete(eventCategories);
            SaveResult<CalendarEventCategories> result = SaveChange(eventCategories);
            result.Entity = eventCategories;

            return result.IsOk;
        }

        public SaveResult<CalendarEventCategories> UpdateEventCategories(CalendarEventCategories eventCategories)
        {
            eventCategories.UpdateDate = DateTime.UtcNow;
            Update(eventCategories);
            var result = SaveChange(eventCategories);
            return result;
        }

        public IQueryable<CalendarEventCategories> GetAllNotDeleted()
        {
            return Filter(x => !x.IsDeleted);
        }

        public CalendarEventCategories GetByIdNotDeleted(int id)
        {
            return Find(x => x.Id == id && !x.IsDeleted);
        }

        public Dictionary<int, string> GetAllKeyValuesNotDeleted()
        {
            return All()
                .Where(x => !x.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}