using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.ExtraFields;
using Group.Salto.SOM.Web.Models.ExtraFieldsTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.CollectionsExtraField
{
    public class CollectionsExtraFieldDetailViewModel : CollectionsExtraFieldViewModel, IValidatableObject
    {
        public CollectionsExtraFieldDetailViewModel()
        {
            ExtraFieldsSelected = new List<ExtraFieldsDetailViewModel>();
        }

        public IList<ExtraFieldsDetailViewModel> ExtraFieldsSelected { get; set; }
        public IList<ExtraFieldsTypesViewModel> ExtraFieldsTypes { get; set; }
        public ExtraFieldsFilterViewModel Filters { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Name) }));
            }

            return errors;
        }
    }
}