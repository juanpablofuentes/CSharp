using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.ExtraFieldsTypes;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.ExtraFields
{
    public class ExtraFieldsDetailViewModel : ExtraFieldsBaseViewModel, IValidatableObject
    {
        public IList<MultiComboViewModel<int, int>> TextTranslations { get; set; }
        public IList<MultiComboViewModel<int, int>> DescriptionTranslations { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.ExtraFieldsName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(NewExtraFieldsRegularName) }));
            }

            return errors;
        }
        public IEnumerable<SelectListItem> ExtraFieldsTypes { get; set; }
        public IList<ExtraFieldsTypesViewModel> ExtraFieldsTypesViewModel { get; set; }
    }
}