using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.PeoplePermissions
{
    public class PeoplePermissionsDto
    {
        public int PeopleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime AssignmentDate { get; set; }
    }
}