using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Sla
{
    public class SlaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MinutesResponse { get; set; }
        public int? MinutesResolutions { get; set; }
        public int? MinutesUnansweredPenalty { get; set; }
        public int? MinutesPenaltyWithoutResolution { get; set; }
    }
}