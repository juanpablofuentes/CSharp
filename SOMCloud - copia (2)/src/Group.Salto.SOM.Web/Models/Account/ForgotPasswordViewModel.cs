using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Account
{
    public class ForgotPasswordViewModel : IValidatableObject
    {
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.Email))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Email) }));
            }
            else if (!string.IsNullOrEmpty(this.Email) && !ValidationsHelper.IsEmailValid(this.Email))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.EmailFormatInvalidMessage),
                    new[] { nameof(Email) }));
            }
            return errors;
        }
    }
}
