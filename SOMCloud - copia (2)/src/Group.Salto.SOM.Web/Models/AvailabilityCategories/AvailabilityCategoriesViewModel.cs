using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.AvailabilityCategories
{
    public class AvailabilityCategoriesViewModel : IValidatableObject
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}