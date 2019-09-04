using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.PredefinedServices
{
    public class PredefinedServicesEditViewModel : IValidatableObject
    {
        public PredefinedServicesEditViewModel()
        {
            Permissions = new MultiSelectViewModel();
        }

        public int PredefinedServicesId { get; set; }
        [MaxLength(50)]
        public string PredefinedServicesName { get; set; }
        public bool PredefinedServicesLinkClosingCode { get; set; }
        public bool PredefinedServicesBillable { get; set; }
        public bool PredefinedServicesMustValidate { get; set; }
        public int CollectionExtraFieldId { get; set; }
        public string CollectionExtraFieldName { get; set; }
        public IEnumerable<SelectListItem> CollectionExtraFieldListItems { get; set; }
        public string PredefinedServicesPermissionsString { get; set; }
        public string PredefinedServicesPermissionsIds { get; set; }
        public string State { get; set; }
        public MultiSelectViewModel Permissions { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(PredefinedServicesName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(PredefinedServicesName) }));
            }
            return errors;
        }
    }
}