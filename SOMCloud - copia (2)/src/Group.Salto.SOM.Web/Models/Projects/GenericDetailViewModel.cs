using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Contracts;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Projects
{
    public class GenericDetailViewModel : IValidatableObject
    {
        public GenericDetailViewModel()
        {
            ContactsSelected = new List<ContactsEditViewModel>();
            Permissions = new MultiSelectViewModel();
        }

        public int Id { get; set; }
        public int ContractId { get; set; }
        public IEnumerable<SelectListItem> ContractListItems { get; set; }
        public string FirstName { get; set; }
        public string Serie { get; set; }
        public int Counter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? VisibilityPda { get; set; }
        public MultiSelectViewModel Permissions { get; set; }
        public int ProjectManagerId { get; set; }
        public IEnumerable<SelectListItem> ProjectManagerListItems { get; set; }
        public int QueueId { get; set; }
        public IEnumerable<SelectListItem> QueueListItems { get; set; }
        public bool IsActive { get; set; }
        public int WOStatusestId { get; set; }
        public IEnumerable<SelectListItem> WOStatusestListItems { get; set; }
        public int ExtraFieldsCollectionId { get; set; }
        public IEnumerable<SelectListItem> ExtraFieldsCollectionListItems { get; set; }
        public int ClosingCodesCollectionId { get; set; }
        public IEnumerable<SelectListItem> ClosingCodesCollectionListItems { get; set; }
        public int WOTypeCollectionId { get; set; }
        public IEnumerable<SelectListItem> WOTypeCollectionListItems { get; set; }
        public int WOCategoriesCollectionId { get; set; }
        public IEnumerable<SelectListItem> WOCategoriesCollectionListItems { get; set; }
        public string Observations { get; set; }
        public string BackOfficeManager { get; set; }
        public string TechnicalSupport { get; set; }
        public string DefaultTechnicianCode { get; set; }
        public IList<ContactsEditViewModel> ContactsSelected { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (ContractId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(ContractId) }));
            }

            if (string.IsNullOrEmpty(this.FirstName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(FirstName) }));
            }

            if (string.IsNullOrEmpty(this.Serie))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Serie) }));
            }

            if (string.IsNullOrEmpty(this.StartDate.ToString()))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(StartDate) }));
            }

            if (string.IsNullOrEmpty(this.EndDate.ToString()))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(EndDate) }));
            }

            if (ProjectManagerId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(ProjectManagerId) }));
            }

            if (WOStatusestId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(WOStatusestId) }));
            }

            if (ClosingCodesCollectionId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(ClosingCodesCollectionId) }));
            }

            if (WOTypeCollectionId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(WOTypeCollectionId) }));
            }

            if (WOCategoriesCollectionId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(WOCategoriesCollectionId) }));
            }

            if (Permissions?.Items?.Count(x => x.IsChecked) == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Permissions) }));
            }

            return errors;
        }
    }
}