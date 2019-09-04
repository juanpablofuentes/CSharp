using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.People
{
    public class AccessEditViewModel : IValidatableObject
    {
        public AccessEditViewModel() { }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string Rol { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public int? NumberEntriesPerPage { get; set; }
        public bool EditMode { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public MultiSelectViewModel Permissions { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.UserName) && string.IsNullOrEmpty(this.Password) && string.IsNullOrEmpty(this.Password2))
            {
                return errors;
            }

            if (string.IsNullOrEmpty(this.UserName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(UserName) }));
            }

            if (!string.IsNullOrEmpty(this.UserName))
            {
                if (!ValidationsHelper.IsEmailValid(this.UserName))
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.EmailFormatInvalidMessage), new[] { nameof(UserName) }));
                }
            }

            if (!EditMode || (EditMode && this.Id == default(Guid)))
            {
                if (string.IsNullOrEmpty(this.Password))
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Password) }));
                }

                if (string.IsNullOrEmpty(this.Password2))
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Password2) }));
                }
            }

            if (!string.IsNullOrEmpty(this.Password) || !string.IsNullOrEmpty(this.Password2))
            {
                if (string.Compare(this.Password, this.Password2, false) != 0)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.UserPassWordNotEqual), new[] { nameof(Password) }));
                }
            }

            return errors;
        }

        public bool IsReadOnly()
        {
            return !string.IsNullOrEmpty(UserName);
        }
    }
}