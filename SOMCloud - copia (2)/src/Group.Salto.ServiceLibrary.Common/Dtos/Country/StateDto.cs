using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Country
{
    public class StateDto : BaseNameIdDto<int>
    {
        public IList<MunicipalityDto> Municipalities { get; set; }
    }
}
