using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.People
{
    public class PeopleCostEditViewModel : IValidatableObject
    {
        public int CostId { get; set; }
        public int PeopleId { get; set; }
        public string HourCost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.HourCost))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(HourCost) }));
            }

            if (!this.StartDate.HasValue)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(StartDate) }));
            }

            if (this.StartDate.HasValue && this.EndDate.HasValue && this.EndDate.Value < this.StartDate.Value)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.EndDateGreaterThanStartDate), new[] { nameof(StartDate) }));
            }

            if (!string.IsNullOrEmpty(this.HourCost) && !HourCost.IsDecimalValue(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DecimalFormatInvalid), new[] { nameof(HourCost) }));
            }
            return errors;
        }
    }
}