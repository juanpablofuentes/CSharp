using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategoryCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoryCalendar;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderCategoryCalendar
{
    public class WorkOrderCategoryCalendarService : BaseService, IWorkOrderCategoryCalendarService
    {
        private readonly IWorkOrderCategoryCalendarRepository _workOrderCategoryCalendarRepository;

        public WorkOrderCategoryCalendarService(ILoggingService logginingService,
                                     IWorkOrderCategoryCalendarRepository workOrderCategoryCalendarRepository) : base(logginingService)
        {
            _workOrderCategoryCalendarRepository = workOrderCategoryCalendarRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoryCalendarRepository)} is null");
        }

        public ResultDto<IList<CalendarDto>> GetByWorkOrderCategoryIdNotDeleted(int workOrderCategoryId)
        {
            IList<Entities.Tenant.WorkOrderCategoryCalendar> calendars = _workOrderCategoryCalendarRepository.GetWorkOrderCategoryCalendarNotDeleted(workOrderCategoryId);

            return ProcessResult(calendars.ToCalendarDto(), calendars != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<WorkOrderCategoryCalendarDto> Create(WorkOrderCategoryCalendarDto model)
        {
            LogginingService.LogInfo($"Creating new WorkOrderCategoryCalendar");
            Entities.Tenant.WorkOrderCategoryCalendar newWorkOrderCategoryCalendar = model.ToEntity();
            var result = _workOrderCategoryCalendarRepository.CreateWorkOrderCategoryCalendar(newWorkOrderCategoryCalendar);
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<WorkOrderCategoryCalendarDto> Update(WorkOrderCategoryCalendarDto model)
        {
            LogginingService.LogInfo($"Update WorkOrderCategoryCalendar with WorkOrderCategoryId {model.WorkOrderCategoryId}, calendarId {model.CalendarId} ");

            ResultDto<WorkOrderCategoryCalendarDto> result = null;
            Entities.Tenant.WorkOrderCategoryCalendar localModel = _workOrderCategoryCalendarRepository.GetByCalendarIdAndCategoryId(model.CalendarId, model.WorkOrderCategoryId);
            if (localModel != null)
            {
                localModel.UpdateWorkOrderCategoryCalendar(model.ToEntity());
                var resultSave = _workOrderCategoryCalendarRepository.UpdateWorkOrderCategoryCalendar(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<WorkOrderCategoryCalendarDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        public ResultDto<bool> Delete(WorkOrderCategoryCalendarDto model)
        {
            LogginingService.LogInfo($"Delete WorkOrderCategoryCalendar with WorkOrderCategoryId {model.WorkOrderCategoryId}, calendarId {model.CalendarId} ");
            ResultDto<bool> result = null;
            Entities.Tenant.WorkOrderCategoryCalendar localModel = _workOrderCategoryCalendarRepository.GetByCalendarIdAndCategoryId(model.CalendarId, model.WorkOrderCategoryId);
            if (localModel != null)
            {
                var resultSave = _workOrderCategoryCalendarRepository.DeleteWorkOrderCategoryCalendar(localModel);
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