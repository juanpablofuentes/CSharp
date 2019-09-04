using Group.Salto.ServiceLibrary.Common.Dtos.SubFamilies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Families
{
    public class FamiliesDetailsDto : FamiliesDto
    {
        public IList<SubFamiliesDto> SubFamiliesList { get; set; }
    }
}