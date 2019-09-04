using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Models.Contracts;

namespace Group.Salto.SOM.Web.Models.FinalClients
{
    public class FinalClientsDetailViewModel : IValidatableObject
    {
        public FinalClientsDetailViewModel()
        {
            ContactsSelected = new List<ContactsEditViewModel>();
        }
        public int Id { get; set; }
        public string IdExtern { get; set; }
        public int OriginId { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string Nif { get; set; }
        [MaxLength(50)]
        public string Phone1 { get; set; }
        [MaxLength(50)]
        public string Phone2 { get; set; }
        [MaxLength(50)]
        public string Phone3 { get; set; }
        [MaxLength(50)]
        public string Fax { get; set; }
        [MaxLength(100)]
        public string Status { get; set; }
        [MaxLength(1000)]
        public string Observations { get; set; }
        public int? PeopleCommercialId { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        public IList<ContactsEditViewModel> Contacts { get; set; }

        public IEnumerable<SelectListItem> ComercialPeople { get; set; }
        public int? SelectedComercialId { get; set; }
        public IEnumerable<SelectListItem> Origins { get; set; }
        public int? SelectedProcedenciaId { get; set; }
        public IList<ContactsEditViewModel> ContactsSelected { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Name) }));
            }
            if (string.IsNullOrEmpty(this.Code))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Code) }));
            }
            if (string.IsNullOrEmpty(this.Nif))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Nif) }));
            }
            if (!this.SelectedComercialId.HasValue || this.SelectedComercialId==0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(SelectedComercialId) }));
            }

            if (!string.IsNullOrEmpty(this.Nif))
            {
                if (!ValidationsHelper.ValidateNIF(this.Nif.Trim()))
                {
                    if (!ValidationsHelper.ValidateNIE(this.Nif.Trim()))
                    {
                        errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NIFInavalidFormat), new[] { nameof(Nif) }));
                    }
                }
            }
            return errors;
        }
    }
}