using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.PredefinedDay
{
    public class PredefinedDayDto
    {
        public int Id { get; set; }
        public PredefinedDayEnum PredefinedType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<PredefinedReasonDto> PredefinedReasons { get; set; }
    }
}
