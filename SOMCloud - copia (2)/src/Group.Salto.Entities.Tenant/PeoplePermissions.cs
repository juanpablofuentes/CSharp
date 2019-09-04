using System;

namespace Group.Salto.Entities.Tenant
{
    public class PeoplePermissions
    {
        public int PeopleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public People People { get; set; }
        public Permissions Permission { get; set; }
    }
}
