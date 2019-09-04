using Group.Salto.Common;
using System.Collections.Generic;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities.Tenant
{
    public class PeopleCollections : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Info { get; set; }

        public ICollection<CalendarPlanningViewConfigurationPeopleCollection> CalendarPlanningViewConfigurationPeopleCollection { get; set; }
        public ICollection<MainWoviewConfigurationsGroups> MainWoviewConfigurationsGroups { get; set; }
        public ICollection<PeopleCollectionCalendars> PeopleCollectionCalendars { get; set; }
        public ICollection<PeopleCollectionsAdmins> PeopleCollectionsAdmins { get; set; }
        public ICollection<PeopleCollectionsPeople> PeopleCollectionsPeople { get; set; }
        public ICollection<PeopleCollectionsPermissions> PeopleCollectionPermission { get; set; }
        public ICollection<PlanningPanelViewConfigurationPeopleCollection> PlanningPanelViewConfigurationPeopleCollection { get; set; }
        public ICollection<Postconditions> Postconditions { get; set; }
        public ICollection<Preconditions> Preconditions { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public ICollection<PushNotificationsPeopleCollections> PushNotificationsPeopleCollections { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
    }
}
