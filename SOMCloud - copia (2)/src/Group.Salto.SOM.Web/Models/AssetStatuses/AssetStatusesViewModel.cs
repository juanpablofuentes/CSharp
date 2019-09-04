using Group.Salto.Common.Constants;
using Group.Salto.Common.Enums;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.AssetStatuses
{
    public class AssetStatusesViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsDefault { get; set; }
        public bool IsRetiredState { get; set; }
        public ModeActionTypeEnum ModeActionType { get; set; }

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