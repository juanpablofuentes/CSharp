using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.People
{
    public class PersonalEditViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhoneEX { get; set; }
        public int Language { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
        public int? UserConfigurationId { get; set; }
        public bool IsVisible { get; set; } = true;

        public string GetFullPersonName()
        {
            return $"{Name} {Surname ?? string.Empty } {SecondSurname ?? string.Empty}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Name) }));
            }

            if (!string.IsNullOrEmpty(this.DNI))
            {
                if (!ValidationsHelper.ValidateNIF(this.DNI.Trim()))
                {
                    if (!ValidationsHelper.ValidateNIE(this.DNI.Trim()))
                    { 
                        errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NIFInavalidFormat), new[] { nameof(DNI) }));
                    }
                }
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