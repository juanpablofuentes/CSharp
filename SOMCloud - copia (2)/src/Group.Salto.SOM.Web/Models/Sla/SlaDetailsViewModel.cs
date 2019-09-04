using Group.Salto.SOM.Web.Models.MultiCombo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Sla
{
    public class SlaDetailsViewModel:SlaViewModel
    {
        public bool? MinutesNaturalResponse { get; set; }
        public bool? MinutesResolutionNaturals { get; set; }
        public bool? MinutesWithoutNaturalResponse { get; set; }
        public int? MinutesPenaltyWithoutResponse { get; set; }
        public bool? MinutesPenaltyWithoutResponseNaturals { get; set; }
        public bool? MinutesPenaltyWithoutNaturalResolution { get; set; }
        public Guid? ReferenceMinutesResponse { get; set; }
        public Guid? ReferenceMinutesResolution { get; set; }
        public Guid? ReferenceMinutesPenaltyUnanswered { get; set; }
        public Guid? ReferenceMinutesPenaltyWithoutResolution { get; set; }
        public bool? TimeResponseActive { get; set; }
        public bool? TimeResolutionActive { get; set; }
        public bool? TimePenaltyWithoutResponseActive { get; set; }
        public bool? TimePenaltyWhithoutResolutionActive { get; set; }
        public bool? MinutesResponseOtDefined { get; set; }
        public bool? MinutesResolutionOtDefined { get; set; }
        public bool? MinutesPenaltyWithoutResponseOtDefined { get; set; }
        public bool? MinutesPenaltyWithoutResolutionOtDefined { get; set; }
        public IEnumerable<SelectListItem> ReferenceTime { get; set; }
        public IList<MultiComboViewModel<int, int>> StatesSla { get; set; }
    }
}