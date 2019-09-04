using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public string CultureCode { get; set; }
        public ICollection<Translation> Translations { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
