using Group.Salto.Controls.Table.Helpers;
using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace Group.Salto.Controls.Interfaces
{
    public interface IMultiselectTableHelperLink<T> where T : class
    {
        bool HasLink { get; set; }
        string Action { get; set; }
        string Controller { get; set; }
        Func<T, object> ParameterFunc { get; set; }
        void SetLinkTemplate<TP>(Func<TableColumnValue<TP>, HelperResult> linkDelegate, string nullValue);
    }
}
