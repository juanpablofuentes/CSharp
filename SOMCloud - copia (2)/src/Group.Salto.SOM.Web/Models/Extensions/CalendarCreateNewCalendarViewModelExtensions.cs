using System;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientSiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientsSiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.ProjectCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoryCalendar;
using Group.Salto.SOM.Web.Models.Calendar;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CalendarCreateNewCalendarViewModelExtensions
    {
        public static AddPeopleCalendarDto ToAddPeopleCalendarDto(this AddCalendarViewModel source)
        {
            AddPeopleCalendarDto result = null;
            if (source != null)
            {
                result = new AddPeopleCalendarDto()
                {
                    PeopleId = source.Id,
                };
                result = ToBase(result, source) as AddPeopleCalendarDto;
            }
            return result;
        }

        public static AddWorkOrderCategoryCalendarDto ToAddWorkOrderCategoryCalendarDto(this AddCalendarViewModel source)
        {
            AddWorkOrderCategoryCalendarDto result = null;
            if (source != null)
            {
                result = new AddWorkOrderCategoryCalendarDto()
                {
                    WorkOrderCategoryId = source.Id,
                };
                result = ToBase(result, source) as AddWorkOrderCategoryCalendarDto;
            }
            return result;
        }

        public static AddProjectCalendarDto ToAddProjectCalendarDto(this AddCalendarViewModel source)
        {
            AddProjectCalendarDto result = null;
            if (source != null)
            {
                result = new AddProjectCalendarDto()
                {
                    ProjectId = source.Id,
                };
                result = ToBase(result, source) as AddProjectCalendarDto;
            }
            return result;
        }

        public static AddFinalClientSiteCalendarDto ToAddFinalClientSiteCalendarDto(this AddCalendarViewModel source)
        {
            AddFinalClientSiteCalendarDto result = null;
            if (source != null)
            {
                result = new AddFinalClientSiteCalendarDto()
                {
                    FinalClientSiteId = source.Id,
                };
                result = ToBase(result, source) as AddFinalClientSiteCalendarDto;
            }
            return result;
        }

        public static AddSiteCalendarDto ToAddSiteCalendarDto(this AddCalendarViewModel source)
        {
            AddSiteCalendarDto result = null;
            if (source != null)
            {
                result = new AddSiteCalendarDto()
                {
                    LocationId = source.Id,
                };
                result = ToBase(result, source) as AddSiteCalendarDto;
            }
            return result;
        }

        public static AddCalendarBaseDto ToBase(AddCalendarBaseDto result, AddCalendarViewModel source)
        {
            result.Name = source.Name;
            result.Description = source.Description;
            result.Color = source.Color;
            result.IsPrivate = source.IsPrivate;
            return result;
        }
    }
}