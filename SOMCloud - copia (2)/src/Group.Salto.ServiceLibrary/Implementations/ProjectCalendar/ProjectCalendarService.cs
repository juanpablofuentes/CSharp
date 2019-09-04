using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ProjectCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.ProjectCalendar;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.ProjectCalendar
{
    public class ProjectCalendarService : BaseService, IProjectCalendarService
    {
        private IProjectCalendarRepository _projectCalendarRepository;

        public ProjectCalendarService(ILoggingService logginingService,
            IProjectCalendarRepository projectCalendarRepository) : base(logginingService)
        {
            _projectCalendarRepository = projectCalendarRepository ?? throw new ArgumentNullException($"{nameof(IProjectCalendarRepository)} is null");
        }

        public ResultDto<IList<CalendarDto>> GetProjectsCalendarsNotDeletedByProjectId(int projectId)
        {
            IList<CalendarDto> projectCalendars = _projectCalendarRepository.GetProjectsCalendarsNotDeletedByProjectId(projectId).ToCalendarDto();
            return ProcessResult(projectCalendars, projectCalendars != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<ProjectCalendarDto> Create(ProjectCalendarDto model)
        {
            LogginingService.LogInfo($"Creating New ProjectCalendar");
            ProjectsCalendars newProjectCalendar = model.ToEntity();
            var result = _projectCalendarRepository.CreateProjectCalendar(newProjectCalendar);
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<ProjectCalendarDto> Update(ProjectCalendarDto model)
        {
            LogginingService.LogInfo($"Update ProjectCalendar with ProjectId {model.ProjectId}, calendarId {model.CalendarId} ");

            ResultDto<ProjectCalendarDto> result = null;
            ProjectsCalendars localModel = _projectCalendarRepository.GetByCalendarIdAndCategoryId(model.CalendarId, model.ProjectId);
            if (localModel != null)
            {
                localModel.UpdateProjectCalendar(model.ToEntity());
                var resultSave = _projectCalendarRepository.UpdateProjectCalendar(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<ProjectCalendarDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        public ResultDto<bool> Delete(ProjectCalendarDto model)
        {
            LogginingService.LogInfo($"Delete ProjectCalendar with ProjectId {model.ProjectId}, calendarId {model.CalendarId} ");
            ResultDto<bool> result = null;
            ProjectsCalendars localModel = _projectCalendarRepository.GetByCalendarIdAndCategoryId(model.CalendarId, model.ProjectId);
            if (localModel != null)
            {
                var resultSave = _projectCalendarRepository.DeleteProjectCalendar(localModel);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };
        }
    }
}