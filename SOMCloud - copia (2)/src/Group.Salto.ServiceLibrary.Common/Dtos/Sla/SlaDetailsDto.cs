using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.StatesSla;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Sla
{
    public class SlaDetailsDto : BaseNameIdDto<int>
    {
        public int? MinutesResponse { get; set; }
        public bool? MinutesNaturalResponse { get; set; }
        public int? MinutesResolutions { get; set; }
        public bool? MinutesResolutionNaturals { get; set; }
        public int? MinutesUnansweredPenalty { get; set; }
        public bool? MinutesWithoutNaturalResponse { get; set; }
        public int? MinutesPenaltyWithoutResponse { get; set; }
        public bool? MinutesPenaltyWithoutResponseNaturals { get; set; }
        public int? MinutesPenaltyWithoutResolution { get; set; }
        public bool? MinutesPenaltyWithoutNaturalResolution { get; set; }
        public bool? TimeResponseActive { get; set; }
        public bool? TimeResolutionActive { get; set; }
        public bool? TimePenaltyWithoutResponseActive { get; set; }
        public bool? TimePenaltyWhithoutResolutionActive { get; set; }
        public bool? MinutesResponseOtDefined { get; set; }
        public bool? MinutesResolutionOtDefined { get; set; }
        public bool? MinutesPenaltyWithoutResponseOtDefined { get; set; }
        public bool? MinutesPenaltyWithoutResolutionOtDefined { get; set; }
         public Guid? ReferenceMinutesPenaltyUnanswered { get; set; }
        public Guid? ReferenceMinutesPenaltyWithoutResolution{ get; set; }
        public Guid? ReferenceMinutesResolution { get; set; }
        public Guid? ReferenceMinutesResponse { get; set; }
        public IEnumerable<SelectListItem> ReferenceTime { get; set; }
        public IList<StatesSlaDto> StatesSla { get; set; }
    }
}