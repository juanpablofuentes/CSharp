using Group.Salto.Common;

namespace Group.Salto.Entities
{
    public class PredefinedReasons : BaseEntity
    {
        public int PredefinedDayStatesId { get; set; }
        public string Name { get; set; }

        public PredefinedDayStates PredefinedDayStates { get; set; }
    }
}
