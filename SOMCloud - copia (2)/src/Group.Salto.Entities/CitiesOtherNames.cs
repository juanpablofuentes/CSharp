using Group.Salto.Common;

namespace Group.Salto.Entities
{
    public class CitiesOtherNames : BaseEntity
    {
        public string Name { get; set; }

        public Cities IdNavigation { get; set; }
    }
}
