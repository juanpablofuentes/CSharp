using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Group.Salto.SOM.Web.Models.MultiSelect;

namespace Group.Salto.SOM.Web.Models.Permissions
{
    public class PermissionDetailViewModel : PermissionListViewModel, IValidatableObject
    {
        public MultiSelectViewModel Projects { get; set; }
        public MultiSelectViewModel Tasks { get; set; }
        public MultiSelectViewModel Queues { get; set; }
        public MultiSelectViewModel PredefinedServices { get; set; }
        public MultiSelectViewModel People { get; set; }
        public MultiSelectViewModel WorkOrderCategories { get; set; }
        public bool CanBeDeleted { get; set; }
        public bool IsLocationResponsible { get; set; }
        public bool IsDriver { get; set; }
        public bool IsContact { get; set; }
        public bool IsComercialTasks { get; set; }
        public bool IsManagerTasks { get; set; }
        public bool IsTechnicalTasks { get; set; }
        public bool IsOperatorTasks { get; set; }
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
