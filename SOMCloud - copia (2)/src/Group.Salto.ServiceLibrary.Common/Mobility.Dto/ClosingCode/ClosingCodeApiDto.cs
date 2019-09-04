using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.ClosingCode
{
    public class ClosingCodeApiDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ClosingCodeApiDto> Childs { get; set; }
    }
}
