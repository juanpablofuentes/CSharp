using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.MultiSelect
{
    public class MultiSelectItem
    {
        public MultiSelectItem() { }
        public string LabelName { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; }
    }
}
