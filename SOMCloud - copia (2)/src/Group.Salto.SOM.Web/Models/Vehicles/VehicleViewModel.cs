using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Vehicles
{
    public class VehicleViewModel : IValidatableObject
    {
        public int? Id { get; set; }
        public string UpdateDate { get; set; }
        public string LicensePlate { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int? PeopleDriverId { get; set; }
        public string Driver { get; set; }

 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.LicensePlate))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                            new[] { nameof(LicensePlate) }));
            }
            return errors;
        }
    }
}