using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.MultiSelect;

namespace Group.Salto.SOM.Web.Models.Queue
{
    public class QueueViewModel : QueueBaseViewModel, IValidatableObject
    {
        public IList<MultiComboViewModel<int,int>> TextTranslations { get; set; }
        public IList<MultiComboViewModel<int, int>> DescriptionTranslations { get; set; }
        public MultiSelectViewModel Permissions { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Name) }));
            }

            if (TextTranslations != null && TextTranslations.Any(x => string.IsNullOrEmpty(x.TextSecondary)))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.TranslationsRequeriredText),
                    new[] { nameof(TextTranslations) }));
            }

            if (DescriptionTranslations != null && DescriptionTranslations.Any(x => string.IsNullOrEmpty(x.TextSecondary)))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.TranslationsRequeriredText),
                    new[] { nameof(DescriptionTranslations) }));
            }

            return errors;
        }
    }
}
