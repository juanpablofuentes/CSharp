using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.People;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.People
{
    public class WorkEditViewModel : IValidatableObject
    {
        public int? CompanyId { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
        public int? WorkCenterId { get; set; }
        public IEnumerable<SelectListItem> WorkCenters { get; set; }
        public int? ResponsibleId { get; set; }
        public int? DepartmentId { get; set; }
        public int? PointRateId { get; set; }
        public IEnumerable<SelectListItem> PointRates { get; set; }
        public int? SubcontractId { get; set; }
        public IEnumerable<SelectListItem> SubContracts { get; set; }
        public bool SubcontractorResponsible { get; set; }
        public int? ProjectId { get; set; }
        public IEnumerable<SelectListItem> Projects { get; set; }
        public string CostKm { get; set; }
        public string DocumentationUrl { get; set; }
        public int? PriorityId { get; set; }
        public IEnumerable<SelectListItem> Priorities { get; set; }
        public bool IsActive { get; set; } = true;
        public int? AnnualHours { get; set; }
        public IList<MultiComboViewModel<int, int>> KnowledgeSelected { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (CompanyId != 0 && SubcontractId != 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleWorkOnlyOneRequiered), new[] { nameof(CompanyId), nameof(SubcontractId) }));
            }

            if (CompanyId == 0 && SubcontractId == 0) 
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleWorkRequiered), new[] { nameof(CompanyId), nameof(SubcontractId) }));
            }

            if (!string.IsNullOrEmpty(this.CostKm) && !CostKm.IsDecimalValue(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DecimalFormatInvalid), new[] { nameof(CostKm) }));
            }

            //TODO:A la espera de decirdir si es necesario o no
            //if (CompanyId != 0 && WorkCenterId != null && (!WorkCenterId.HasValue || WorkCenterId.Value == 0))
            //{
            //    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(WorkCenterId) }));
            //}

            //if (CompanyId != 0 && WorkCenterId != null && (!ResponsibleId.HasValue || ResponsibleId.Value == 0))
            //{
            //    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(ResponsibleId) }));
            //}

            return errors;
        }
    }
}
