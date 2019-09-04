using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.ExtraFieldsTypes
{
    public class ExtraFieldsTypesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMandatoryVisibility { get; set; }
        public bool AllowedValuesVisibility { get; set; }
        public bool MultipleChoiceVisibility { get; set; }
        public bool ErpSystemVisibility { get; set; }
    }
}