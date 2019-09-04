using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.SalesRate
{
    public class SalesRateViewModel: IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReferenceERP { get; set; }

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
