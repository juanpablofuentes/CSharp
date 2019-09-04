using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Zones
{
    public class ZonesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ZoneProjectDto> ZoneProject { get; set; }
    }
}