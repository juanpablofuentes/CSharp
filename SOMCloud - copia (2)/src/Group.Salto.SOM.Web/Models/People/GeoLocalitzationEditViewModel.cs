using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.People
{
    public class GeoLocalitzationEditViewModel : IValidatableObject
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string WorkRadiusKm { get; set; }
        public string Apikey { get; set; }
        public string Language { get; set; } = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Latitude))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Latitude) }));
            }

            if (string.IsNullOrEmpty(this.Longitude))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Longitude) }));
            }

            if (string.IsNullOrEmpty(this.WorkRadiusKm))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(WorkRadiusKm) }));
            }
            return errors;
        }
    }
}