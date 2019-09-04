using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Contracts
{
    public class ContactsEditViewModel : IValidatableObject
    {
        public int ContactsId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string FirstSurname { get; set; }
        [MaxLength(50)]
        public string SecondSurname { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Telephone { get; set; }
        [MaxLength(50)]
        public string Position { get; set; }
        public string FullName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Name) }));
            }

            if (!ValidationsHelper.IsTextNameValid(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.InvalidNameCharacters), new[] { nameof(Name) }));
            }

            if (!ValidationsHelper.IsTextNameValid(this.FirstSurname))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.InvalidNameCharacters), new[] { nameof(FirstSurname) }));
            }

            if (!ValidationsHelper.IsTextNameValid(this.SecondSurname))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.InvalidNameCharacters), new[] { nameof(SecondSurname) }));
            }

            if (!string.IsNullOrEmpty(this.Email))
            {
                if (!ValidationsHelper.IsEmailValid(this.Email))
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.EmailFormatInvalidMessage), new[] { nameof(Email) }));
                }
            }

            return errors;
        }
    }
}