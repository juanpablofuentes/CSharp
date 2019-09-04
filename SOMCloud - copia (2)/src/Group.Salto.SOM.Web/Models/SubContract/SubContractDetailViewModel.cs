using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group.Salto.SOM.Web.Models.SubContract
{
    public class SubContractDetailViewModel : SubContractViewModel, IValidatableObject
    {
        public int? PurchaseRateId { get; set; }
        public IList<MultiComboViewModel<int, int>> PeopleSelected { get; set; }
        public IList<MultiComboViewModel<int, int>> ResponsiblesSelected { get; set; }

        public IList<MultiComboViewModel<int,int>> KnowledgeSelected { get; set; }
        public IEnumerable<SelectListItem> PurchaseRate { get; set; }
        public IEnumerable<SelectListItem> Priorities { get; set; }

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