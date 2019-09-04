using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ProjectCalendarRepository : BaseRepository<ProjectsCalendars>, IProjectCalendarRepository
    {
        public ProjectCalendarRepository(ITenantUnitOfWork uow) : base(uow) { }

        public IList<ProjectsCalendars> GetProjectsCalendarsNotDeletedByProjectId(int projectId)
        {
            return Filter(pc => pc.ProjectId == projectId && !pc.Calendar.IsDeleted)                
                .Include(c => c.Calendar)
                    .ThenInclude(ce => ce.CalendarEvents)
                .OrderByDescending(o => o.CalendarPriority)
                .ToList();
        }

        public ProjectsCalendars GetByCalendarIdAndCategoryId(int calendarId, int categoryId)
        {
            return Find(x => x.ProjectId == categoryId && x.CalendarId == calendarId);
        }

        public IList<ProjectsCalendars> GetProjectPreferenceCalendar(int projectId)
        {
            return Filter(pc => pc.ProjectId == projectId && !pc.Calendar.IsDeleted)
                    .Include(c => c.Calendar)
                        .ThenInclude(c => c.CalendarEvents)
                    .OrderByDescending(o => o.CalendarPriority)
                    .ToList();
        }

        public SaveResult<ProjectsCalendars> CreateProjectCalendar(ProjectsCalendars entity)
        {
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<ProjectsCalendars> UpdateProjectCalendar(ProjectsCalendars entity)
        {
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<ProjectsCalendars> DeleteProjectCalendar(ProjectsCalendars entity)
        {
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}