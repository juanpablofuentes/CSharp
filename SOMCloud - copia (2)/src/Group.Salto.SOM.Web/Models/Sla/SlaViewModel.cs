using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Sla
{
    public class SlaViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MinutesResponse { get; set; }
        public int? MinutesResolutions { get; set; }
        public int? MinutesUnansweredPenalty { get; set; }
        public int? MinutesPenaltyWithoutResolution { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                            new[] { nameof(Name) }));
            }
            return errors;
        }
    }
}