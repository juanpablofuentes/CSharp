using Group.Salto.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group.Salto.Entities.Tenant
{
    public class SiteUser : BaseEntity
    {
        public string Name { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Position { get; set; }
        public int LocationId { get; set; }
        [NotMapped]
        public string FullName { get { return $"{Name} {FirstSurname} {SecondSurname}"; } }

        public Locations Location { get; set; }
        public ICollection<Assets> Assets { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
    }
}
