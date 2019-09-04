using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Country;

namespace Group.Salto.ServiceLibrary.Common.Dtos
{
    public class CountryDto : BaseNameIdDto<int>
    {
        public IList<RegionDto> Regions { get; set; }
    }
}