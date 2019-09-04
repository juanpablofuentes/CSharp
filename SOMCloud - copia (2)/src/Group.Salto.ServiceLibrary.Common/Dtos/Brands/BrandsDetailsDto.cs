using Group.Salto.ServiceLibrary.Common.Dtos.Team;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Brand
{
   public class BrandsDetailsDto: BrandsDto
    {
        public IList<ModelDto> Models { get; set; }
    }
}