using Group.Salto.Controls.Table.Helpers;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.Controls.Interfaces
{
    public interface IMultiselectTableHelperField<TRoot> where TRoot : class
    {
        string Name { get; }
        int Width { get; set; }
        bool IsSortable { get; set; }
        IMultiselectTableHelperLink<TRoot> Link { get; }
        void SetTemplate(Func<TableColumnValue, HelperResult> templateDelegate, string nullValue);
        void SetTemplate(Func<TableColumnValue, HelperResult> templateDelegate);
        void SetTemplate<TP>(Func<TableColumnValue<TP>, HelperResult> templateDelegate, string nullValue);
        void SetTemplate<TP>(Func<TableColumnValue<TP>, HelperResult> templateDelegate);
    }
}
