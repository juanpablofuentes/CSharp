using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Technicians
{
    public class TechniciansEditViewModel : IValidatableObject
    {
        public int TechniciansId { get; set; }
        public int PeopleId { get; set; }
        public IEnumerable<SelectListItem> PeopleListItems { get; set; }
        public string TechniciansName { get; set; }
        [MaxLength(100)]
        public string Code { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (this.PeopleId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(PeopleId) }));
            }
            return errors;
        }
    }
}