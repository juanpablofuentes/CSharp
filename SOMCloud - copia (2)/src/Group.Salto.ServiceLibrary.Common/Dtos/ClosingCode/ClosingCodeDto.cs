using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ClosingCode
{
    public class ClosingCodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ClosingCodeDto> Childs { get; set; }
        public int IdClonedItem { get; set; }
    }
}