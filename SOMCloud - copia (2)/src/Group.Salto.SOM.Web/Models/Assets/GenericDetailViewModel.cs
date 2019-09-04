using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.Assets
{
    public class GenericDetailViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string StockNumber { get; set; }
        public string AssetNumber { get; set; }
        public string Observations { get; set; }
        public List<SelectListItem> Contracts { get; set; }
        public List<SelectListItem> Statuses { get; internal set; }
        public int? ContractId { get; set; }
        public int StatusId { get; set; }
        public int? FromSiteId { get; set; }
        public MultiComboViewModel<int?, string> SubFamily { get; set; }
        public MultiComboViewModel<int?, string> Family { get; set; }
        public MultiComboViewModel<int?, string> Model { get; set; }
        public MultiComboViewModel<int?, string> Brand { get; set; }
        public MultiComboViewModel<int?, string> SiteUser { get; set; }
        public MultiComboViewModel<int, string> SiteLocation { get; set; }
        public MultiComboViewModel<int?, string> SiteClient { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.AssetNumber) &&
                string.IsNullOrEmpty(this.SerialNumber) &&
                string.IsNullOrEmpty(this.StockNumber))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                            new[] { nameof(SerialNumber) }));
            }
            return errors;
        }

    }
}