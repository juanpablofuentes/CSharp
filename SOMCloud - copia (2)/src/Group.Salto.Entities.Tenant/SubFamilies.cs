using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class SubFamilies : BaseEntity
    {
        public int FamilyId { get; set; }
        public string Nom { get; set; }
        public string Descripcio { get; set; }

        public Families Family { get; set; }
        public ICollection<Assets> Assets { get; set; }
    }
}
