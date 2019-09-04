using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.SymptomCollection
{
    public class SymptomCollectionDetailViewModel : SymptomCollectionViewModel, IValidatableObject
    {
        public MultiSelectViewModel SymptomsSelected { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Name) }));
            }
           
            return errors;
        }
    }
}