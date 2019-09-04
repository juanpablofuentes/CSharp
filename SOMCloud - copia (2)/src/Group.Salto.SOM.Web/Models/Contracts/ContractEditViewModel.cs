using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Contracts
{
    public class ContractEditViewModel : IValidatableObject
    {
        public ContractEditViewModel()
        {
            ContactsSelected = new List<ContactsEditViewModel>();
        }
        public int Id { get; set; }
        [MaxLength(100)]
        public string Object { get; set; }
        [MaxLength(1000)]
        public string Reference { get; set; }
        public int ContractTypeId { get; set; }
        public IEnumerable<SelectListItem> ContractTypes { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [MaxLength(1000)]
        public string PhysicalAddress { get; set; }
        [MaxLength(100)]
        public string Signer { get; set; }
        public bool Active { get; set; }
        [MaxLength(1000)]
        public string Observations { get; set; }
        public int? ClientId { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }
        public int? PeopleId { get; set; }
        public IEnumerable<SelectListItem> People { get; set; }
        public IList<ContactsEditViewModel> ContactsSelected { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Object))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Object) }));
            }

            if (this.ContractTypeId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(ContractTypeId) }));
            }

            if (this.StartDate.HasValue && this.EndDate.HasValue && this.EndDate.Value < this.StartDate.Value)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.EndDateGreaterThanStartDate), new[] { nameof(StartDate) }));
            }

            return errors;
        }
    }
}