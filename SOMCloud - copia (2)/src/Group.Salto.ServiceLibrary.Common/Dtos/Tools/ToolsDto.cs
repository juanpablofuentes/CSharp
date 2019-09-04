using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Tools
{
    public class ToolsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int ? VehicleId { get; set; }
        public string VehicleName { get; set; }
    }
}
