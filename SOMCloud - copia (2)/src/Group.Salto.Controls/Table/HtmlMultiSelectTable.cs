using Group.Salto.Controls.Table.Models;
using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Group.Salto.Controls.Entities;
using Group.Salto.Controls.Interfaces;
using Group.Salto.Controls.Table.Helpers;

namespace Group.Salto.Controls.Table
{
    public static class HtmlMultiSelectTable
    {
        public static MultiselectTableHelper<T, TK> GetMultiSelectTableFor<T, TK>(
            this IHtmlHelper helper,
            MultiItemViewModel<T, TK> model,
            Expression<Func<T, TK>> keyProperty,
            ISortableFilter filter,
            ITranslationTable translation = null) where T : class
        {
            return new MultiselectTableHelper<T, TK>(model, keyProperty, helper, filter, translation);
        }
    }
}
