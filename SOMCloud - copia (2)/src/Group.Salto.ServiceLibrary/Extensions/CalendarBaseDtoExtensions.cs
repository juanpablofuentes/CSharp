using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientsSiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.ProjectCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoryCalendar;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CalendarBaseDtoExtensions
    {
        public static CalendarBaseDto ToDto(this Calendars source)
        {
            CalendarBaseDto result = null;
            if (source != null)
            {
                result = new CalendarBaseDto();
                source.ToCalendarDto(result);
            }

            return result;
        }

        public static AddPeopleCalendarDto ToPrivatePeopleCalendarDto(this Calendars source)
        {
            AddPeopleCalendarDto result = null;
            if (source != null)
            {
                result = new AddPeopleCalendarDto();
                source.ToPrivateCalendarDto(result);
            }

            return result;
        }

        public static AddWorkOrderCategoryCalendarDto ToPrivateWorkOrderCategoryCalendarDto(this Calendars source)
        {
            AddWorkOrderCategoryCalendarDto result = null;
            if (source != null)
            {
                result = new AddWorkOrderCategoryCalendarDto();
                source.ToPrivateCalendarDto(result);
            }

            return result;
        }

        public static AddProjectCalendarDto ToPrivateProjectCalendarDto(this Calendars source)
        {
            AddProjectCalendarDto result = null;
            if (source != null)
            {
                result = new AddProjectCalendarDto();
                source.ToPrivateCalendarDto(result);
            }

            return result;
        }

        public static AddFinalClientSiteCalendarDto ToPrivateFinalClientSiteCalendarDto(this Calendars source)
        {
            AddFinalClientSiteCalendarDto result = null;
            if (source != null)
            {
                result = new AddFinalClientSiteCalendarDto();
                source.ToPrivateCalendarDto(result);
            }

            return result;
        }

        public static AddSiteCalendarDto ToPrivateSiteCalendarDto(this Calendars source)
        {
            AddSiteCalendarDto result = null;
            if (source != null)
            {
                result = new AddSiteCalendarDto();
                source.ToPrivateCalendarDto(result);
            }

            return result;
        }

        public static Calendars ToEntity(this CalendarBaseDto source)
        {
            Calendars result = null;
            if (source != null)
            {
                result = new Calendars()
                {
                    Name = source.Name,
                    Color = source.Color,
                    Description = source.Description,
                    IsPrivate = source.IsPrivate,
                };
            }

            return result;
        }

        public static Calendars ToPrivateCalendarEntity(this AddPeopleCalendarDto source)
        {
            Calendars result = null;
            if (source != null)
            {
                result = new Calendars()
                {
                    Name = source.Name,
                    Color = source.Color,
                    Description = source.Description,
                    IsPrivate = source.IsPrivate
                };
                result.PeopleCalendars = result.PeopleCalendars ?? new List<PeopleCalendars>();
                result.PeopleCalendars.Add(new PeopleCalendars() { PeopleId = source.PeopleId, CalendarPriority = 0 });
            }
            return result;
        }

        public static Calendars ToPrivateCalendarEntity(this AddWorkOrderCategoryCalendarDto source)
        {
            Calendars result = null;
            if (source != null)
            {
                result = new Calendars()
                {
                    Name = source.Name,
                    Color = source.Color,
                    Description = source.Description,
                    IsPrivate = source.IsPrivate
                };
                result.WorkOrderCategoryCalendar = result.WorkOrderCategoryCalendar ?? new List<WorkOrderCategoryCalendar>();
                result.WorkOrderCategoryCalendar.Add(new WorkOrderCategoryCalendar() { WorkOrderCategoryId = source.WorkOrderCategoryId, CalendarPriority = 0 });
            }
            return result;
        }

        public static Calendars ToPrivateCalendarEntity(this AddProjectCalendarDto source)
        {
            Calendars result = null;
            if (source != null)
            {
                result = new Calendars()
                {
                    Name = source.Name,
                    Color = source.Color,
                    Description = source.Description,
                    IsPrivate = source.IsPrivate
                };
                result.ProjectsCalendars = result.ProjectsCalendars ?? new List<ProjectsCalendars>();
                result.ProjectsCalendars.Add(new ProjectsCalendars() { ProjectId = source.ProjectId, CalendarPriority = 0 });
            }
            return result;
        }

        public static Calendars ToPrivateCalendarEntity(this AddFinalClientSiteCalendarDto source)
        {
            Calendars result = null;
            if (source != null)
            {
                result = new Calendars()
                {
                    Name = source.Name,
                    Color = source.Color,
                    Description = source.Description,
                    IsPrivate = source.IsPrivate
                };
                result.FinalClientSiteCalendar = result.FinalClientSiteCalendar ?? new List<FinalClientSiteCalendar>();
                result.FinalClientSiteCalendar.Add(new FinalClientSiteCalendar() { FinalClientSiteId = source.FinalClientSiteId, CalendarPriority = 0 });
            }
            return result;
        }

        public static Calendars ToPrivateCalendarEntity(this AddSiteCalendarDto source)
        {
            Calendars result = null;
            if (source != null)
            {
                result = new Calendars()
                {
                    Name = source.Name,
                    Color = source.Color,
                    Description = source.Description,
                    IsPrivate = source.IsPrivate
                };
                result.LocationCalendar = result.LocationCalendar ?? new List<LocationCalendar>();
                result.LocationCalendar.Add(new LocationCalendar() { LocationId = source.LocationId, CalendarPriority = 0 });
            }
            return result;
        }

        public static void ToCalendarDto(this Calendars source, CalendarBaseDto target)
        {
            if (target != null && source != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
                target.Color = source.Color;
                target.IsPrivate = source.IsPrivate.HasValue ? source.IsPrivate.Value : false;
                target.HasPeopleAssigned = (source.PeopleCalendars?.Any() ?? false) || (source.PeopleCollectionCalendars?.Any() ?? false);
            }
        }

        public static void ToPrivateCalendarDto(this Calendars source, AddPeopleCalendarDto target)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Color = source.Color;
                target.IsPrivate = source.IsPrivate.HasValue ? source.IsPrivate.Value : false;
                target.PeopleId = (source.PeopleCalendars != null) ? source.PeopleCalendars.FirstOrDefault().PeopleId: 0;
            }
        }

        public static void ToPrivateCalendarDto(this Calendars source, AddSiteCalendarDto target)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Color = source.Color;
                target.IsPrivate = source.IsPrivate.HasValue ? source.IsPrivate.Value : false;
                target.LocationId = (source.LocationCalendar != null) ? source.LocationCalendar.FirstOrDefault().LocationId : 0;
            }
        }

        public static void ToPrivateCalendarDto(this Calendars source, AddWorkOrderCategoryCalendarDto target)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Color = source.Color;
                target.IsPrivate = source.IsPrivate.HasValue ? source.IsPrivate.Value : false;
                target.WorkOrderCategoryId = (source.WorkOrderCategoryCalendar != null) ? source.WorkOrderCategoryCalendar.FirstOrDefault().WorkOrderCategoryId : 0;
            }
        }

        public static void ToPrivateCalendarDto(this Calendars source, AddProjectCalendarDto target)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Color = source.Color;
                target.IsPrivate = source.IsPrivate.HasValue ? source.IsPrivate.Value : false;
                target.ProjectId = (source.ProjectsCalendars != null) ? source.ProjectsCalendars.FirstOrDefault().ProjectId : 0;
            }
        }

        public static void ToPrivateCalendarDto(this Calendars source, AddFinalClientSiteCalendarDto target)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Color = source.Color;
                target.IsPrivate = source.IsPrivate.HasValue ? source.IsPrivate.Value : false;
                target.FinalClientSiteId = (source.FinalClientSiteCalendar != null) ? source.FinalClientSiteCalendar.FirstOrDefault().FinalClientSiteId : 0;
            }
        }

        public static IList<CalendarBaseDto> ToDto(this IQueryable<Calendars> source)
        {
            return source?.MapList(x => x.ToDto()).ToList();
        }

        public static void Update(this Calendars target, CalendarBaseDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Color = source.Color;
                target.Description = source.Description;
                target.IsPrivate = source.IsPrivate;
            }
        }
    }
}