using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.FinalClientSite;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientSiteCalendar;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.FinalClientSiteCalendars
{
    public class FinalClientSiteCalendarServices : BaseService, IFinalClientSiteCalendarServices
    {
        private IFinalClientSiteCalendarRepository _finalClientSiteCalendarRepository;

        public FinalClientSiteCalendarServices(ILoggingService logginingService,
            IFinalClientSiteCalendarRepository finalClientSiteCalendarRepository) : base(logginingService)
        {
            _finalClientSiteCalendarRepository = finalClientSiteCalendarRepository ?? throw new ArgumentNullException($"{nameof(IFinalClientSiteCalendarRepository)} is null");
        }

        public ResultDto<IList<CalendarDto>> GetFinalClientSiteCalendarsNotDeletedByFinalClientSiteId(int finalClientsId)
        {
            IList<CalendarDto> finalClientSiteCalendars = _finalClientSiteCalendarRepository.GetFinalClientSiteCalendarsNotDeletedByFinalClientSiteId(finalClientsId).ToCalendarDto();
            return ProcessResult(finalClientSiteCalendars, finalClientSiteCalendars != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<FinalClientSiteCalendarDto> Create(FinalClientSiteCalendarDto model)
        {
            LogginingService.LogInfo($"Creating New finalClientsCalendar");
            FinalClientSiteCalendar newfinalClientsCalendar = model.ToEntity();
            var result = _finalClientSiteCalendarRepository.CreateFinalClientSiteCalendar(newfinalClientsCalendar);
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<FinalClientSiteCalendarDto> Update(FinalClientSiteCalendarDto model)
        {
            LogginingService.LogInfo($"Update finalClientsCalendar with finalClientsId {model.FinalClientSiteId}, calendarId {model.CalendarId} ");

            ResultDto<FinalClientSiteCalendarDto> result = null;
            FinalClientSiteCalendar localModel = _finalClientSiteCalendarRepository.GetByCalendarIdAndCategoryId(model.CalendarId, model.FinalClientSiteId);
            if (localModel != null)
            {
                localModel.UpdateFinalClientSiteCalendar(model.ToEntity());
                var resultSave = _finalClientSiteCalendarRepository.UpdateFinalClientSiteCalendar(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<FinalClientSiteCalendarDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        public ResultDto<bool> Delete(FinalClientSiteCalendarDto model)
        {
            LogginingService.LogInfo($"Delete finalClientsCalendar with finalClientsId {model.FinalClientSiteId}, calendarId {model.CalendarId} ");
            ResultDto<bool> result = null;
            FinalClientSiteCalendar localModel = _finalClientSiteCalendarRepository.GetByCalendarIdAndCategoryId(model.CalendarId, model.FinalClientSiteId);
            if (localModel != null)
            {
                var resultSave = _finalClientSiteCalendarRepository.DeleteFinalClientSiteCalendar(localModel);
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