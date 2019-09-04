using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Common.Dtos.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.MultiSelect;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Task
{
    public class TaskHeaderDetailViewModel : TaskHeaderViewModel
    {
        public int FlowId { get; set; }
        public IList<MultiComboViewModel<int, int>> TextTranslations { get; set; }
        public IList<MultiComboViewModel<int, int>> DescriptionTranslations { get; set; }
        //This multiselect if for TaskEdit
        public MultiSelectViewModel ModalMultiselect { get; set; }

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
