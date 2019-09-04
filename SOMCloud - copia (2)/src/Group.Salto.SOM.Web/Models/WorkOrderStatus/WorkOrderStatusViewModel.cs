using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;

namespace Group.Salto.SOM.Web.Models.WorkOrderStatus
{
    public class WorkOrderStatusViewModel : WorkOrderStatusBaseViewModel, IValidatableObject
    {
        public IList<MultiComboViewModel<int,int>> TextTranslations { get; set; }
        public IList<MultiComboViewModel<int, int>> DescriptionTranslations { get; set; }
        public string Color { get; set; }
        public bool IsWorkOrderClosed { get; set; }
        public bool IsPlannable { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Name) }));
            }

            if (string.IsNullOrEmpty(this.Color))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Color) }));
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
