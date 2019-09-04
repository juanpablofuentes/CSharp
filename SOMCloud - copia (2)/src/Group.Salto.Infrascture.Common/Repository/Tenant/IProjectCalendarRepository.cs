using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IProjectCalendarRepository
    {
        IList<ProjectsCalendars> GetProjectsCalendarsNotDeletedByProjectId(int projectId);
        ProjectsCalendars GetByCalendarIdAndCategoryId(int calendarId, int categoryId);
        IList<ProjectsCalendars> GetProjectPreferenceCalendar(int projectId);
        SaveResult<ProjectsCalendars> CreateProjectCalendar(ProjectsCalendars entity);
        SaveResult<ProjectsCalendars> UpdateProjectCalendar(ProjectsCalendars entity);
        SaveResult<ProjectsCalendars> DeleteProjectCalendar(ProjectsCalendars entity);
    }
}