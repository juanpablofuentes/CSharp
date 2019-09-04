using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;

namespace Group.Salto.SOM.Web.Models.Company
{
    public class CompanyViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CostKm { get; set; }
        public bool HasPeopleAssigned { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Name)}));
            }
            //TODO review formats
            if (!string.IsNullOrEmpty(this.CostKm) && !CostKm.IsDecimalValue(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DecimalFormatInvalid),
                    new[] { nameof(CostKm) }));
            }
            return errors;
        }
    }
}
