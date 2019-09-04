using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientSiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.ProjectCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoryCalendar;
using Group.Salto.SOM.Web.Models.Calendar;
using Group.Salto.SOM.Web.Models.People;
using System;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PrivateCalendarViewModelExtensions
    {
        public static PeopleCalendarDto ToPeopleCalendarDto(this PrivateCalendarsViewModel source)
        {
            PeopleCalendarDto result = null;
            if (source != null)
            {
                result = new PeopleCalendarDto()
                {
                    PeopleId = source.Id,
                    CalendarId = source.CalId,
                    Priority = source.Priority
                };
            }
            return result;
        }

        public static WorkOrderCategoryCalendarDto ToWorkOrderCategoryCalendarDto(this PrivateCalendarsViewModel source)
        {
            WorkOrderCategoryCalendarDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoryCalendarDto()
                {
                    WorkOrderCategoryId = source.Id,
                    CalendarId = source.CalId,
                    Priority = source.Priority
                };
            }
            return result;
        }

        public static ProjectCalendarDto ToProjectCalendarDto(this PrivateCalendarsViewModel source)
        {
            ProjectCalendarDto result = null;
            if (source != null)
            {
                result = new ProjectCalendarDto()
                {
                    ProjectId = source.Id,
                    CalendarId = source.CalId,
                    Priority = source.Priority
                };
            }
            return result;
        }

        public static FinalClientSiteCalendarDto ToFinalClientSiteCalendarDto(this PrivateCalendarsViewModel source)
        {
            FinalClientSiteCalendarDto result = null;
            if (source != null)
            {
                result = new FinalClientSiteCalendarDto()
                {
                    FinalClientSiteId = source.Id,
                    CalendarId = source.CalId,
                    Priority = source.Priority
                };
            }
            return result;
        }

        public static SiteCalendarDto ToSiteCalendarDto(this PrivateCalendarsViewModel source)
        {
            SiteCalendarDto result = null;
            if (source != null)
            {
                result = new SiteCalendarDto()
                {
                    SiteId = source.Id,
                    CalendarId = source.CalId,
                    Priority = source.Priority
                };
            }
            return result;
        }
    }
}