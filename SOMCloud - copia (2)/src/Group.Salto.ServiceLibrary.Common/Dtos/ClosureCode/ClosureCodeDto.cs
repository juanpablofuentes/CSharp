using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.ClosingCode;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ClosureCode
{
    public class ClosureCodeDto : ClosureCodeBaseDto
    {
        public IList<ClosingCodeDto> ClosingCodes { get; set; }
    }
}