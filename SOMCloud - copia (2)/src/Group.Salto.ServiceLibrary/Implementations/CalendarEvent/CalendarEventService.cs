using System;
using System.Collections.Generic;
using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CalendarEvent;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;
using Group.Salto.ServiceLibrary.Extensions;
using System.Linq;
using Group.Salto.Common.Constants.CalendarEvent;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;

namespace Group.Salto.ServiceLibrary.Implementations.CalendarEvent
{
    public class CalendarEventService : BaseService, ICalendarEventService
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly ICalendarRepository _calendarRepository;

        public CalendarEventService(ILoggingService logginingService, 
                                    ICalendarEventRepository calendarEventRepository,
                                    ICalendarRepository calendarRepository) : base(logginingService)
        {
            _calendarEventRepository = calendarEventRepository ?? throw new ArgumentNullException($"{nameof(ICalendarEventRepository)} is null");
            _calendarRepository = calendarRepository ?? throw new ArgumentNullException($"{nameof(ICalendarRepository)} is null");
        }

        public ResultDto<CalendarDto> GetByCalendarId(int calendarId)
        {
            Calendars calendar = _calendarRepository.GetCalendarAndEventsById(calendarId);

            return ProcessResult(calendar.ToCalendarPeopleDto(0), calendar != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<CalendarEventDto> Create(CalendarEventDto calendarEvent)
        {
            LogginingService.LogInfo($"Creating new CalendarEvent");
            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidateCalendarEvents(calendarEvent, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                CalendarEvents newCalendarEvent = calendarEvent.ToEntity();
                SaveResult<CalendarEvents> result = _calendarEventRepository.CreateCalendarEvent(newCalendarEvent);
                return ProcessResult(result.Entity?.ToSchedulerDto(), result);
            }

            return ProcessResult(calendarEvent, errors);
        }

        public ResultDto<CalendarEventDto> Update(CalendarEventDto calendarEvent)
        {
            LogginingService.LogInfo($"Update CalendarEvent with id = {calendarEvent.Id}");

            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidateCalendarEvents(calendarEvent, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                CalendarEvents localCalendarEvent = _calendarEventRepository.GetById(calendarEvent.Id);

                if (localCalendarEvent != null)
                {
                    ResultDto<CalendarEventDto> result = null;

                    if (localCalendarEvent != null)
                    {
                        localCalendarEvent.UpdateCalendarEvent(calendarEvent.ToEntity());
                        SaveResult<CalendarEvents> resultRepository = _calendarEventRepository.UpdateCalendarEvent(localCalendarEvent);
                        result = ProcessResult(resultRepository.Entity?.ToSchedulerDto(), resultRepository);
                    }
                }
                else
                {
                    errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = CalendarEventConstants.CalendarEventNotExist });
                }
            }

            return ProcessResult(calendarEvent, errors);
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete Client by id {id}");
            ResultDto<bool> result = null;
            CalendarEvents localclient = _calendarEventRepository.GetById(id);
            if (localclient != null)
            {
                SaveResult<CalendarEvents> resultSave = _calendarEventRepository.DeleteCalendarEvent(localclient);
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

        private bool ValidateCalendarEvents(CalendarEventDto calendarEvent, out List<ErrorDto> result)
        {
            result = new List<ErrorDto>();
            if (!calendarEvent.IsValid())
            {
                result.Add(new ErrorDto()
                {
                    ErrorType = ErrorType.ValidationError,
                });
            }

            return !result.Any();
        }
    }
}