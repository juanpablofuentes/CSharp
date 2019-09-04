using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.Items
{
    public class ItemGeneralViewModel : ItemViewModel,IValidatableObject
    {
        public bool TrackSerialNumber { get; set; }
        public bool InDepot { get; set; }
        public MultiComboViewModel<int?, string> SubFamily { get; set; }
        public MultiComboViewModel<int?, string> Family { get; set; }   
        public IFormFile  Picture { get; set; }
        public int? AvailableQuantity { get; set; }//TODO calculate this
        public List<SelectListItem> ItemTypes { get; set; }
        public string Thumbnail {get;set;}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Name) ||
                string.IsNullOrEmpty(this.InternalReference))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                            new[] { nameof(Name),nameof(InternalReference) }));
            }
            return errors;
        }
    }
}