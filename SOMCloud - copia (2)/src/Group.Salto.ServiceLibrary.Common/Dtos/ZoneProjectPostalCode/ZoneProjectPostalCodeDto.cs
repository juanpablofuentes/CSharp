using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProject;
using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ZoneProjectPostalCode
{
    public class ZoneProjectPostalCodeDto
    {
        public int PostalCodeId { get; set; }
        public int ZoneProjectId { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public DateTime UpdateTime { get; set; }
        public ZoneProjectDto ZoneProject { get; set; }
    }
}