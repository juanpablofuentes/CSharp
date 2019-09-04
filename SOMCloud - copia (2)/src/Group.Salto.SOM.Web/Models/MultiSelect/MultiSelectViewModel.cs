using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.MultiSelect
{
    public class MultiSelectViewModel
    {
        public MultiSelectViewModel()
        {
            Items = new List<MultiSelectItem>();
        }

        public MultiSelectViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public IList<MultiSelectItem> Items { get; set; }
    }
}