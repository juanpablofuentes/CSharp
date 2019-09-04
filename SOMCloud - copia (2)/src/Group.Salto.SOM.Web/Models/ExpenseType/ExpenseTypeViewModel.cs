using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.ExpenseType
{
    public class ExpenseTypeViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Type))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                            new[] { nameof(Type) }));
            }
            return errors;
        }
    }
}