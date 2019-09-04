using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.Common;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCalendar;

namespace Group.Salto.ServiceLibrary.Implementations.PeopleCalendar
{
    public class PeopleCalendarService : BaseService, IPeopleCalendarService
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IPeopleCalendarsRepository _peopleCalendarRepository;

        public PeopleCalendarService(ILoggingService logginingService,
                                     ICalendarRepository calendarRepository,
                                     IPeopleCalendarsRepository peopleCalendarRepository) : base(logginingService)
        {
            _calendarRepository = calendarRepository ?? throw new ArgumentNullException($"{nameof(ICalendarRepository)} is null");
            _peopleCalendarRepository = peopleCalendarRepository ?? throw new ArgumentNullException($"{nameof(IPeopleCalendarsRepository)} is null");
        }

        public ResultDto<IList<CalendarDto>> GetByPeopleIdNotDeleted(int peopleId)
        {
            IList<PeopleCalendars> calendars = _peopleCalendarRepository.GetPeopleCalendarNotDeleted(peopleId);

            return ProcessResult(calendars.ToCalendarDto(), calendars != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<PeopleCalendarDto> Create(PeopleCalendarDto model)
        {
            LogginingService.LogInfo($"Creating new PeopleCalendar");
            PeopleCalendars newPeopleCalendar = model.ToEntity();
            var result = _peopleCalendarRepository.CreateCalendarPeople(newPeopleCalendar);
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<PeopleCalendarDto> Update(PeopleCalendarDto model)
        {
            LogginingService.LogInfo($"Update PeopleCalendar with peopleId {model.PeopleId}, calendarId {model.CalendarId} ");

            ResultDto<PeopleCalendarDto> result = null;
            PeopleCalendars localModel = _peopleCalendarRepository.GetByCalendarIdAndPeopleId(model.CalendarId, model.PeopleId);
            if (localModel != null)
            {
                localModel.UpdatePeopleCalendar(model.ToEntity());
                var resultSave = _peopleCalendarRepository.UpdateCalendarPeople(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<PeopleCalendarDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        public ResultDto<bool> Delete(PeopleCalendarDto model)
        {
            LogginingService.LogInfo($"Delete PeopleCalendar with peopleId {model.PeopleId}, calendarId {model.CalendarId} ");
            ResultDto<bool> result = null;
            PeopleCalendars localModel = _peopleCalendarRepository.GetByCalendarIdAndPeopleId(model.CalendarId, model.PeopleId);
            if (localModel != null)
            {
                var resultSave = _peopleCalendarRepository.DeleteCalendarPeople(localModel);
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