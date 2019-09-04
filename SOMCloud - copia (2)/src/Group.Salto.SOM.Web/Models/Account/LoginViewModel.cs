using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;

namespace Group.Salto.SOM.Web.Models.Account
{
    public class LoginViewModel : IValidatableObject
    {
        public string Email { get; set; }

        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        [DataType(DataType.Url)]
        public string ReturnUrl { get; set; }

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
            if (string.IsNullOrEmpty(this.Password?.Trim()))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Password) }));
            }
            return errors;
        }
    }
}
