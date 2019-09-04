using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Permissions : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int CanBeDeleted { get; set; }
        public string Tasks { get; set; }

        public ICollection<WorkOrderCategoryPermissions> WorkOrderCategoryPermission { get; set; }
        public ICollection<PredefinedServicesPermission> PredefinedServicesPermission { get; set; }
        public ICollection<PermissionsTasks> PermissionTask { get; set; }
        public ICollection<PermissionsQueues> PermissionQueue { get; set; }
        public ICollection<PeopleCollectionsPermissions> PeopleCollectionPermission { get; set; }
        public ICollection<ProjectsPermissions> ProjectPermission { get; set; }
        public ICollection<PeoplePermissions> PeoplePermission { get; set; }

    }
}
