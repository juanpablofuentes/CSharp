using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientsSiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.ProjectCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoryCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientSiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteCalendar;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Calendar
{
    public interface ICalendarService
    {
        ResultDto<IList<CalendarBaseDto>> GetAllFiltered(CalendarFilterDto filter);
        ResultDto<CalendarBaseDto> GetById(int id);
        ResultDto<CalendarBaseDto> Create(CalendarBaseDto model);
        ResultDto<CalendarBaseDto> Update(CalendarBaseDto model);
        ResultDto<bool> Delete(int id);
        ResultDto<bool> Delete(PeopleCalendarDto model);
        ResultDto<bool> Delete(WorkOrderCategoryCalendarDto model);
        ResultDto<bool> Delete(ProjectCalendarDto model);
        ResultDto<bool> Delete(FinalClientSiteCalendarDto model);
        ResultDto<bool> Delete(SiteCalendarDto model);
        BaseNameIdDto<int> GetKeyValuesById(int Id);
        IList<BaseNameIdDto<int>> GetKeyValuesAvailablePeopleCalendarsToAssign(int peopleId);
        IList<BaseNameIdDto<int>> GetKeyValuesAvailableWorkOrderCategoryCalendarsToAssign(int workOrderCategoryId);
        IList<BaseNameIdDto<int>> GetKeyValuesAvailableFinalClientsSiteCalendarsToAssign(int finalClientSiteId);
        IList<BaseNameIdDto<int>> GetKeyValuesAvailableSiteCalendarsToAssign(int SiteId);
        ResultDto<AddPeopleCalendarDto> AddPrivateCalendar(AddPeopleCalendarDto model);
        ResultDto<AddWorkOrderCategoryCalendarDto> AddPrivateCalendar(AddWorkOrderCategoryCalendarDto model);
        ResultDto<AddProjectCalendarDto> AddPrivateCalendar(AddProjectCalendarDto model);
        ResultDto<AddFinalClientSiteCalendarDto> AddPrivateCalendar(AddFinalClientSiteCalendarDto model);
        ResultDto<AddSiteCalendarDto> AddPrivateCalendar(AddSiteCalendarDto model);
        IList<BaseNameIdDto<int>> GetKeyValuesAvailableProjectCalendarsToAssign(int projectId);
    }
}