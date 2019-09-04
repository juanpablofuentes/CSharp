using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Projects;
using Group.Salto.SOM.Web.Models.ZoneProject;
using Group.Salto.SOM.Web.Models.ZoneProjectPostalCode;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Zones
{
    public class ZoneViewModel : IValidatableObject
    {
        public ZoneViewModel()
        {            
            ZonesProjects = new List<ZoneProjectViewModel>();
            Projects = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ZoneProjectViewModel> ZonesProjects { get; set; }
        public IList<SelectListItem> Projects { get; set; }
        public IList<ZoneProjectPostalCodeViewModel> SelectedPostalCodes { get; set; }
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