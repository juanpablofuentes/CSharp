using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.WorkCenter
{
    public class WorkCenterDetailViewModel : IValidatableObject
    {
        public WorkCenterViewModel WorkCenter { get; set; }
        public IList<KeyValuePair<int, string>> Countries { get; set; }
        public int? CountrySelected { get; set; }
        public int? StateSelected { get; set; }
        public int? RegionSelected { get; set; }
        public int? MunicipalitySelected { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
        public int? CompanySelected { get; set; }
        public IEnumerable<SelectListItem> People { get; set; }
        public int? ResponsableSelected { get; set; }
        public int? WorkCenterFromCompanyId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (CompanySelected == null && WorkCenterFromCompanyId == null)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                            new[] { nameof(CompanySelected) }));
            }
            
            return errors;
        }
    }
}