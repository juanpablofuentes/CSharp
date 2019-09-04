using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.ExtraFields
{
    public class ExtraFieldsBaseViewModel
    {
        public int ExtraFieldsId { get; set; }
        public string ExtraFieldsName { get; set; }
        public int ExtraFieldsSystemId { get; set; }
        public string ExtraFieldsSystemName { get; set; }
        public IEnumerable<SelectListItem> ExtraFieldsSystemListItems { get; set; }
        public int ExtraFieldsRegularId { get; set; }
        public string ExtraFieldsRegularName { get; set; }
        public IEnumerable<SelectListItem> ExtraFieldsRegularListItems { get; set; }
        public string NewExtraFieldsRegularName { get; set; }
        public string ExtraFieldsDescription { get; set; }
        public bool IsMandatory { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public IEnumerable<SelectListItem> TypeListItems { get; set; }
        public string AllowedStringValues { get; set; }
        public bool MultipleChoice { get; set; }
        public int? ErpSystemInstanceQueryId { get; set; }
        public string ErpSystemInstanceQueryName { get; set; }
        public IEnumerable<SelectListItem> ErpSystemInstanceQueryListItems { get; set; }
        public bool DelSystem { get; set; }
        public int? Position { get; set; }
        public string State { get; set; }
    }
}