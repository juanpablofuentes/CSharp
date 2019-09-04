using System;

namespace Group.Salto.MigrationData.Model
{
    public class SlaMigration
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
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
        public string ReferenceMinutesResponse { get; set; }
        public string ReferenceMinutesResolution { get; set; }
        public string ReferenceMinutesPenaltyUnanswered { get; set; }
        public string ReferenceMinutesPenaltyWithoutResolution { get; set; }
    }
}