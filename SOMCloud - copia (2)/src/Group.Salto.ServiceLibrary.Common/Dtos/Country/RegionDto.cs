using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Country
{
    public class RegionDto : BaseNameIdDto<int>
    {
        public IList<StateDto> States;
    }
}
