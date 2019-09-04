using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Sites;
using Group.Salto.ServiceLibrary.Common.Contracts.SitesCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteCalendar;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.SitesCalendar
{
    public class SitesCalendarService : BaseService, ISitesCalendarServices
    {
        private ISiteCalendarRepository _siteCalendarRepository;

        public SitesCalendarService(ILoggingService logginingService,
            ISiteCalendarRepository siteCalendarRepository) : base(logginingService)
        {
            _siteCalendarRepository = siteCalendarRepository ?? throw new ArgumentNullException($"{nameof(IFinalClientSiteCalendarRepository)} is null");
        }

        public ResultDto<IList<CalendarDto>> GetSitesCalendarsNotDeleted_BySiteId(int siteId)
        {
            IList<CalendarDto> SiteCalendars = _siteCalendarRepository.GetSiteCalendarsNotDeletedBySiteId(siteId).ToCalendarDto();
            
            return ProcessResult(SiteCalendars, SiteCalendars != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<SiteCalendarDto> Create(SiteCalendarDto model)
        {
            LogginingService.LogInfo($"Creating New finalClientsCalendar");
            LocationCalendar newSiteCalendar = model.ToEntity();
            var result = _siteCalendarRepository.CreateSiteCalendar(newSiteCalendar);
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<SiteCalendarDto> Update(SiteCalendarDto model)
        {
            LogginingService.LogInfo($"Update SiteCalendar with SiteId {model.SiteId}, calendarId {model.CalendarId} ");

            ResultDto<SiteCalendarDto> result = null;
            LocationCalendar localModel = _siteCalendarRepository.GetByCalendarIdAndCategoryId(model.CalendarId, model.SiteId);
            if (localModel != null)
            {
                localModel.UpdateSiteCalendar(model.ToEntity());
                var resultSave = _siteCalendarRepository.UpdateSiteCalendar(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<SiteCalendarDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        public ResultDto<bool> Delete(SiteCalendarDto model)
        {
            LogginingService.LogInfo($"Delete siteCalendar with siteId {model.SiteId}, calendarId {model.CalendarId} ");
            ResultDto<bool> result = null;
            LocationCalendar localModel = _siteCalendarRepository.GetByCalendarIdAndCategoryId(model.CalendarId, model.SiteId);
            if (localModel != null)
            {
                var resultSave = _siteCalendarRepository.DeleteSiteCalendar(localModel);
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