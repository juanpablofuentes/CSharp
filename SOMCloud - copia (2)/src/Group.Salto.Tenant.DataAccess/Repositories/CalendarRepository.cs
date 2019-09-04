using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class CalendarRepository : BaseRepository<Calendars>, ICalendarRepository
    {
        public CalendarRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Calendars> GetAllGlobals()
        {
            return Filter(x => (!x.IsPrivate.HasValue || !x.IsPrivate.Value) && !x.IsDeleted, GetIncludePeopleAndPeopleCollection());
        }

        public Calendars GetCalendarAndEventsById(int id)
        {
            return Find(x => x.Id == id && !x.IsDeleted, GetIncludeEvents());
        }

        public Calendars GetById(int id)
        {
            return Find(x => x.Id == id && !x.IsDeleted, GetIncludePeopleAndPeopleCollection());
        }

        public IList<Calendars> GetGlobalPreferenceCalendar(int id)
        {
            return Filter(x => x.Id == id && !x.IsDeleted).ToList();
        }

        public Dictionary<int, string> GetKeyValuesAvailablePeopleCalendarsToAssign(int peopleId)
        {
            IQueryable<Calendars> query = Filter(x => (!x.IsPrivate.HasValue || !x.IsPrivate.Value) && !x.IsDeleted, GetIncludesPredicate(new List<Type>() { typeof(PeopleCalendars) }));
            query = query.Where(x => !x.PeopleCalendars.Any(pc => pc.PeopleId == peopleId));
            query = query.OrderBy(o => o.Id);
            return query.ToDictionary(t => t.Id, t => t.Name);
        }

        public Dictionary<int, string> GetKeyValuesAvailableWorkOrderCategoryCalendarsToAssign(int workOrderCategoryId)
        {
            IQueryable<Calendars> query = Filter(x => (!x.IsPrivate.HasValue || !x.IsPrivate.Value) && !x.IsDeleted, GetIncludesPredicate(new List<Type>() { typeof(WorkOrderCategoryCalendar) }));
            query = query.Where(x => !x.WorkOrderCategoryCalendar.Any(pc => pc.WorkOrderCategoryId == workOrderCategoryId));
            query = query.OrderBy(o => o.Id);
            return query.ToDictionary(t => t.Id, t => t.Name);
        }

        public Dictionary<int, string> GetKeyValuesAvailableProjectCalendarsToAssign(int projectId)
        {
            IQueryable<Calendars> query = Filter(x => (!x.IsPrivate.HasValue || !x.IsPrivate.Value) && !x.IsDeleted, GetIncludesPredicate(new List<Type>() { typeof(ProjectsCalendars) }));
            query = query.Where(x => !x.ProjectsCalendars.Any(pc => pc.ProjectId == projectId));
            query = query.OrderBy(o => o.Id);
            return query.ToDictionary(t => t.Id, t => t.Name);
        }

        public Dictionary<int, string> GetKeyValuesAvailableFinalClientSiteCalendarsToAssign(int finalClientSiteId)
        {
            IQueryable<Calendars> query = Filter(x => (!x.IsPrivate.HasValue || !x.IsPrivate.Value) && !x.IsDeleted, GetIncludesPredicate(new List<Type>() { typeof(FinalClientSiteCalendar) }));
            query = query.Where(x => !x.FinalClientSiteCalendar.Any(pc => pc.FinalClientSiteId == finalClientSiteId));
            query = query.OrderBy(o => o.Id);
            return query.ToDictionary(t => t.Id, t => t.Name);
        }

        public Dictionary<int, string> GetKeyValuesAvailableSiteCalendarsToAssign(int siteId)
        {
            IQueryable<Calendars> query = Filter(x => (!x.IsPrivate.HasValue || !x.IsPrivate.Value) && !x.IsDeleted, GetIncludesPredicate(new List<Type>() { typeof(LocationCalendar) }));
            query = query.Where(x => !x.LocationCalendar.Any(pc => pc.LocationId == siteId));
            query = query.OrderBy(o => o.Id);
            return query.ToDictionary(t => t.Id, t => t.Name);
        }

        public Calendars GetByIdWithEventsAndPeople(int id)
        {
            return Find(x => x.Id == id && !x.IsDeleted, GetIncludesPredicate(new List<Type>()
                { typeof(PeopleCalendars), typeof(PeopleCollectionCalendars), typeof(CalendarEvents) }));
        }

        public Calendars GetByIdWithEventsAndWorkOrderCategoryCalendar(int id)
        {
            return Find(x => x.Id == id && !x.IsDeleted, GetIncludesPredicate(new List<Type>()
                { typeof(WorkOrderCategoryCalendar), typeof(CalendarEvents) }));
        }

        public Calendars GetByIdWithEventsAndProjectCalendar(int id)
        {
            return Find(x => x.Id == id && !x.IsDeleted, GetIncludesPredicate(new List<Type>()
                { typeof(ProjectsCalendars), typeof(CalendarEvents) }));
        }

        public SaveResult<Calendars> UpdateCalendar(Calendars entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<Calendars> CreateCalendar(Calendars entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<Calendars> DeleteCalendar(Calendars entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public Calendars DeleteOnContext(Calendars entity)
        {
            Delete(entity);
            return entity;
        }

       
               
        private List<Expression<Func<Calendars, object>>> GetIncludePeopleAndPeopleCollection()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(PeopleCalendars), typeof(PeopleCollectionCalendars) });
        }

        private List<Expression<Func<Calendars, object>>> GetIncludePeopleAndEvents()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(PeopleCalendars), typeof(CalendarEvents) });
        }

        private List<Expression<Func<Calendars, object>>> GetIncludeEvents()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(CalendarEvents) });
        }

        private List<Expression<Func<Calendars, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Calendars, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(PeopleCalendars))
                {
                    includesPredicate.Add(p => p.PeopleCalendars);
                }
                if (element == typeof(PeopleCollectionCalendars))
                {
                    includesPredicate.Add(p => p.PeopleCollectionCalendars);
                }
                if (element == typeof(CalendarEvents))
                {
                    includesPredicate.Add(p => p.CalendarEvents);
                }
                if (element == typeof(WorkOrderCategoryCalendar))
                {
                    includesPredicate.Add(p => p.WorkOrderCategoryCalendar);
                }
                if (element == typeof(ProjectsCalendars))
                {
                    includesPredicate.Add(p => p.ProjectsCalendars);
                }
            }
            return includesPredicate;
        }
    }
}