using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Model;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Brands
{
    public class ModelViewModel : IValidatableObject
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }
        public string ModelUrl { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(ModelName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                            new[] { nameof(ModelName) }));
            }

            if (!string.IsNullOrEmpty(ModelUrl))
            {
                var validUrl = ValidationsHelper.ValidateUrl(ModelUrl);
                if (!validUrl)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(ModelConstants.ModelCreateErrorMessage),
                                new[] { nameof(ModelUrl) }));
                }
            }

            return errors;
        }
    }
}
