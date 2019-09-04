using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoriesCollection;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.WorkOrderCategory;

namespace Group.Salto.SOM.Web.Models.WorkOrderCategoriesCollection
{
    public class WorkOrderCategoriesCollectionBaseViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public IList<WorkOrderCategoryBaseViewModel> CategoriesSelected { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Name) }));
            }

            return errors;
        }
    }
}