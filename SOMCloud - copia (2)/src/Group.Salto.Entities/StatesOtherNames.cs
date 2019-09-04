using Group.Salto.Common;

namespace Group.Salto.Entities
{
    public class StatesOtherNames : BaseEntity
    {
        public string Name { get; set; }

        public States IdNavigation { get; set; }
    }
}
