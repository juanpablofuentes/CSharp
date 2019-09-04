using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.PerformTask
{
    public class PerformTaskFormsViewModel : IValidatableObject
    {
        public int ExtraFieldsId { get; set; }
        public string ExtraFieldsName { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public int TypeId { get; set; }
        public string Observations { get; set; }
        public string AllowedStringValues { get; set; }
        public bool MultipleChoice { get; set; }
        public int? ErpSystemInstanceQueryId { get; set; }
        public bool DelSystem { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //TODO:Walter
            return null;
        }
    }
}
