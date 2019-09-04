using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProjectPostalCode;
using Group.Salto.ServiceLibrary.Common.Dtos.Zones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ZoneProject
{
    public class ZoneProjectDto
    {
        public int ZoneProjectId { get; set; }
        public int ZoneId { get; set; }
        public int? ProjectId { get; set; }
        public ProjectsDto Project { get; set; }
        public ZonesDto Zone { get; set; }
        public IList<ZoneProjectPostalCodeDto> PostalCodes { get; set; }
        public IList<ProjectsDto> ProjectsList { get; set; }
        public string State { get; set; } 

    }
}
