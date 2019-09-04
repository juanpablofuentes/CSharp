using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Account
{
    public class ResetPasswordViewModel : IValidatableObject
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.Password))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Password) }));
            }

            if (string.IsNullOrEmpty(this.ConfirmPassword))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(ConfirmPassword) }));
            }

            if (!string.IsNullOrEmpty(this.Password) && !string.IsNullOrEmpty(this.ConfirmPassword))
            {
                if (string.Compare(this.Password, this.ConfirmPassword, false) != 0)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.UserPassWordNotEqual), new[] { nameof(Password) }));
                }
            }
            return errors;
        }
    }
}