using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Group.Salto.SOM.Web.Models.WorkOrderCategory
{
    public class GenericEditViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public int WorkOrderCategoriesCollectionId { get; set; }
        public string Name { get; set; }
        public int IdSLA { get; set; }
        public IEnumerable<SelectListItem> SLAListItems { get; set; }
        public IEnumerable<SelectListItem> WOCollectionListItems { get; set; }
        public string Url { get; set; }
        public string Duration { get; set; }
        public string EconomicCharge { get; set; }
        public string Serie { get; set; }
        public bool IsGhost { get; set; }
        public string DefaultTechnicalCode { get; set; }
        public int ClientSiteCalendarPreference { get; set; }
        public int ProjectCalendarPreference { get; set; }
        public int CategoryCalendarPreference { get; set; }
        public int SiteCalendarPreference { get; set; }
        public string BackOfficeResponsible { get; set; }
        public string TechnicalResponsible { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Name) }));
            }

            if (this.WorkOrderCategoriesCollectionId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(WorkOrderCategoriesCollectionId) }));
            }

            if (this.IdSLA == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(IdSLA) }));
            }

            if (string.IsNullOrEmpty(this.Duration))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Duration) }));
            }

            if (!string.IsNullOrEmpty(this.Duration) && !Duration.IsDecimalValue(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DecimalFormatInvalid), new[] { nameof(Duration) }));
            }

            if (string.IsNullOrEmpty(this.EconomicCharge))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(EconomicCharge) }));
            }

            if (!string.IsNullOrEmpty(this.EconomicCharge) && !EconomicCharge.IsDecimalValue(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DecimalFormatInvalid), new[] { nameof(EconomicCharge) }));
            }

            return errors;
        }
    }
}