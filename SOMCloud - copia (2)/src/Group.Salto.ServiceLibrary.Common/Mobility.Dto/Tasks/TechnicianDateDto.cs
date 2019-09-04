using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks
{
    public class TechnicianDateDto
    {
        public int TechnicianId { get; set; }
        public bool FixedTechnician { get; set; }
        public DateTime ActuationDate { get; set; }
        public bool FixedDate { get; set; }
    }
}
