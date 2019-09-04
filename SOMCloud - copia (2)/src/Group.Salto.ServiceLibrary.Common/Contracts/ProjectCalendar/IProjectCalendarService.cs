using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.ProjectCalendar;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ProjectCalendar
{
    public interface IProjectCalendarService
    {
        ResultDto<IList<CalendarDto>> GetProjectsCalendarsNotDeletedByProjectId(int projectId);
        ResultDto<ProjectCalendarDto> Create(ProjectCalendarDto model);
        ResultDto<ProjectCalendarDto> Update(ProjectCalendarDto model);
        ResultDto<bool> Delete(ProjectCalendarDto model);
    }
}