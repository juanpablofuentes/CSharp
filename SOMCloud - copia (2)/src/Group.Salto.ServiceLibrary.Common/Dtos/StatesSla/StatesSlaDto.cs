using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.StatesSla
{
    public class StatesSlaDto: BaseNameIdDto<int>
    {
        public int SlaId { get; set; }
        public int? MinutesToTheEnd { get; set; }
        public string RowColor { get; set; }
    }
}