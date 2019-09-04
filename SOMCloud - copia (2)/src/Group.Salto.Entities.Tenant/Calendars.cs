using System.Collections.Generic;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities.Tenant
{
    public class Calendars : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public bool? IsPrivate { get; set; }

        public ICollection<CalendarEvents> CalendarEvents { get; set; }
        public ICollection<FinalClientSiteCalendar> FinalClientSiteCalendar { get; set; }
        public ICollection<LocationCalendar> LocationCalendar { get; set; }
        public ICollection<PeopleCalendars> PeopleCalendars { get; set; }
        public ICollection<PeopleCollectionCalendars> PeopleCollectionCalendars { get; set; }
        public ICollection<PlanificationProcesses> PlanificationProcesses { get; set; }
        public ICollection<ProjectsCalendars> ProjectsCalendars { get; set; }
        public ICollection<WorkOrderCategoriesCollectionCalendar> WorkOrderCategoriesCollectionCalendar { get; set; }
        public ICollection<WorkOrderCategoryCalendar> WorkOrderCategoryCalendar { get; set; }
    }
}