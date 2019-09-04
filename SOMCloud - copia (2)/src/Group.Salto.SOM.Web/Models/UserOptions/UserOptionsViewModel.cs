using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.UserOptions
{
    public class UserOptionsViewModel : IValidatableObject
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public int LanguageId { get; set; }
        public int NumberEntriesPerPageId { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
        public IEnumerable<SelectListItem> NumberEntriesPerPage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (!string.IsNullOrEmpty(OldPassword) && string.IsNullOrEmpty(NewPassword))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(NewPassword) }));
            }

            if (!string.IsNullOrEmpty(NewPassword) && string.IsNullOrEmpty(OldPassword))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(OldPassword) }));
            }

            if (!string.Equals(NewPassword, ConfirmNewPassword))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.UserPassWordNotEqual), new[] { nameof(NewPassword) }));
            }

            return errors;
        }
    }
}