using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group.Salto.Controls.Table.Helpers
{
    /// <summary>
    /// This class is used as a parameter for the razor templates used in the MultiSelectTableHelper.
    /// </summary>
    public class TableColumnValue
    {
        /// <summary>
        /// Value of the property being generated as a string
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Name of the property being generated
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Column index
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Raw value of the property
        /// </summary>
        public object RawValue { get; set; }

        //TODO: Antes IHtmalString
        public TagBuilder LinkHtml { get; set; }
    }

    public class TableColumnValue<T> : TableColumnValue
    {
        public new T RawValue
        {
            get { return (T)base.RawValue; }
            set { base.RawValue = value; }
        }
    }
}
