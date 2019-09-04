using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Permisions
{
    public class PermissionDetailDto : PermissionsDto
    {
        public int CanBeDeleted { get; set; }
        public string TasksChecked { get; set; }
        public IList<int> WorkOrdersCategories { get; set; }
        public IList<int> Queues { get; set; }
        public IList<int> PredefinedServices { get; set; }
        public IList<int> People { get; set; }
        public IList<int> Projects { get; set; }
        public IList<int> Tasks { get; set; }
    }
}