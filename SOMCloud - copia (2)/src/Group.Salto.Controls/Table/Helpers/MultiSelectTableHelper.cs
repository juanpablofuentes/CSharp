using Group.Salto.Controls.Entities;
using Group.Salto.Controls.Interfaces;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using Group.Salto.Controls.Table.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using HtmlString = Microsoft.AspNetCore.Html.HtmlString;

namespace Group.Salto.Controls.Table.Helpers
{
    public class MultiSelectTableHelper
    {
        public MultiSelectTableHelper()
        {
            Id = Guid.NewGuid().ToString();
            ShowSelectColumn = true;
            ShowHeader = true;
        }

        public MultiSelectTableHelper(ISortableFilter filterToShow)
            : this()
        {
            Filter = filterToShow;
        }

        /// <summary>
        /// If true sorting headers are generated
        /// </summary>
        public bool AllowSorting { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// If <c>true</c> an edit link is shown in every row
        /// </summary>
        public bool ShowEditLink { get; set; }

        public bool ShowHeader { get; set; }

        public bool ShowSelectColumn { get; set; }

        /// <summary>
        /// CSS classes applied to the [table] tag.
        /// If not specified "table table-striped" is used
        /// </summary>
        public string TableCssClass { get; set; }

        public string TableHeaderCssClass { get; set; }
        public bool UseKeyPropertyLikeAEditLink { get; set; }

        protected ISortableFilter Filter { get; }

        protected ITranslationTable Translation { get; set; }
    }

    /// <summary>
    /// This class renders a basic table where every row is a object of type [T]
    /// </summary>
    /// <typeparam name="T">Type of the object shown</typeparam>
    public class MultiselectTableHelper<T, TK> : MultiSelectTableHelper
        where T : class
    {
        private readonly MultiItemViewModel<T, TK> data;

        private readonly Dictionary<string, MultiSelectTableHelperField<T>> fields;

        private readonly IHtmlHelper htmlHelper;

        private readonly PropertyInfo keyProperty;

        /// <summary>
        /// Build a new MultiSelectTableHelper object
        /// </summary>
        /// <param name="data">Data to show</param>
        /// <param name="keyProperty">Property used as a ID of each item.</param>
        /// <param name="helper">HtmlHelper used. Needed to generate links in the same view context of the view</param>
        /// <param name="filter">The filter to be used.</param>
        public MultiselectTableHelper(
            MultiItemViewModel<T, TK> data,
            Expression<Func<T, TK>> keyProperty,
            IHtmlHelper helper,
            ISortableFilter filter,
            ITranslationTable translation)
            : base(filter)
        {
            this.data = data;
            TableCssClass = "table table-hover";
            if (keyProperty.Body is MemberExpression memberExpression)
            {
                this.keyProperty = memberExpression.Member as PropertyInfo;
            }

            ShowEditLink = false;
            UseKeyPropertyLikeAEditLink = true;
            htmlHelper = helper;
            AllowSorting = true;
            Translation = translation;
            fields = new Dictionary<string, MultiSelectTableHelperField<T>>();
        }

        /// <summary>
        /// Column template to allow customization of every column generated.
        /// If null a standard template [td]value[/td] is used.
        /// <remarks>
        /// If used the [td]...[/td] must be specified!
        /// </remarks>
        /// </summary>
        public Func<TableColumnValue, HelperResult> DefaultColumnTemplate { get; set; }

        public void AddColumnTemplateFor<TR>(
            Expression<Func<T, TR>> prop,
            Func<TableColumnValue<TR>, HelperResult> template, string headerText = null)
        {
            AddColumnTemplateFor(prop, template, string.Empty, headerText);
        }

        public void AddColumnTemplateFor<TR>(
             Expression<Func<T, TR>> prop,
             Func<TableColumnValue<TR>, HelperResult> template,
             string nullValue, string headerText)
        {
            var propInfo = ExpressionExtensions<T>.AsPropertyInfo(prop);
            var tinfo = new TemplateInfo { Delegate = template, NullValue = nullValue };
            GetOrCreateField(propInfo, headerText).Template = tinfo;
        }

        public IMultiselectTableHelperFieldStatic<T> AddStaticColumn(string colname)
        {
            var newcol = new MultiSelectTableHelperFieldStatic<T>(colname);
            fields.Add(colname, newcol);
            return newcol;
        }

        public void GenerateAllColumns()
        {
            fields.Clear();
            foreach (var field in GetFieldsToDisplay())
            {
                fields.Add(field.Name, field);
            }
        }

        public IMultiselectTableHelperField<T> GetFieldFor<TR>(Expression<Func<T, TR>> prop)
        {
            var name = ExpressionExtensions<T>.AsPropertyName(prop);
            return GetField(name);
        }

        /// <summary>
        /// Renders the whole table
        /// </summary>
        /// <returns>HTML string to be inserted in the view</returns>
        public IHtmlContent Render(string className)
        {
            var writer = new TagBuilder("table");
            writer.Attributes.Add("id", Id);
            writer.Attributes.Add("class", $"{TableCssClass} {className}");
            writer.InnerHtml.AppendHtml(AddHead());
            writer.InnerHtml.AppendHtml(AddBody());
            //writer.InnerHtml.Append(string.Concat(AddHead(), AddBody()));
            //writer.InnerHtml = string.Concat(AddHead(), AddBody());
            return writer;
        }

        /// <summary>
        /// Set the columns to display
        /// </summary>
        /// <param name="cols">Columns to display</param>
        public void SetColumns(params Expression<Func<T, dynamic>>[] cols)
        {
            foreach (var col in cols)
            {
                MemberExpression memberExp;
                if (col.Body is UnaryExpression body)
                {
                    memberExp = body.Operand as MemberExpression;
                }
                else
                {
                    memberExp = col.Body as MemberExpression;
                }

                if (memberExp == null)
                {
                    continue;
                }

                var member = memberExp.Member as PropertyInfo;
                GetOrCreateField(member);
            }
        }

        public void SetSortingColumns(params Expression<Func<T, dynamic>>[] columns)
        {
            var names = columns.Select(expr => ExpressionExtensions<T>.AsPropertyName(expr)).ToList();
            foreach (var field in fields.Values)
            {
                field.IsSortable = names.Contains(field.Name);
            }
        }

        private static TableColumnValue CreateTableValueForField(
            T item,
            MultiSelectTableHelperField<T> field,
            int index)
        {
            var tableValue = field.CreateTableValueObject();
            tableValue.Value = field.GetValueText(item);
            tableValue.Name = field.Name;
            tableValue.Index = index;
            tableValue.RawValue = field.GetRawValue(item);
            return tableValue;
        }

        /// <summary>
        /// Generates the [tbody] tag and its contents
        /// </summary>
        /// <returns>Html content of the whole [tbody]</returns>
        private TagBuilder AddBody()
        {
            var writer = new TagBuilder("tbody");
            foreach (var item in data.Items)
            {
                writer.InnerHtml.AppendHtml(AddItemRow(item));
            }
            //writer.InnerHtml = innerHtml.ToString();
            return writer;
        }

        private TagBuilder AddDefaultTemplateItemValueColumn(T item, MultiSelectTableHelperField<T> field, int index)
        {
            TagBuilder linkHtml = field.Link.HasLink ? AddEditLink(item, field, index) : new TagBuilder("span");
            var columnValue = new TableColumnValue
            {
                Value = field.GetValueText(item),
                Name = field.Name,
                Index = index,
                RawValue = field.GetRawValue(item),
                LinkHtml = linkHtml,
            };
            var tagBuilder = new TagBuilder("span");
            tagBuilder.InnerHtml.AppendHtml(DefaultColumnTemplate(columnValue));
            return tagBuilder;
        }

        /// <summary>
        /// Generate the edit column of a item
        /// </summary>
        /// <param name="item">Item being generated</param>
        /// <returns>Html code of the edit column</returns>
        private TagBuilder AddEditColumn(T item)
        {
            var td = new TagBuilder("td");
            var id = keyProperty.GetValue(item).ToString();
            var urlHelper = new UrlHelper(this.htmlHelper.ViewContext);
            var url = urlHelper.Action("Edit", new { id = id });
            var anchor = new TagBuilder("a");
            anchor.Attributes.Add("href", url);
            anchor.InnerHtml.Append("Edit");
            td.InnerHtml.AppendHtml(anchor);
            return td;
        }

        /// <summary>
        /// Generates the edit header
        /// </summary>
        /// <returns>Html code of the edit header</returns>
        private string AddEditHeader()
        {
            var th = new TagBuilder("th");
            th.InnerHtml.Append("Edit");
            return th.ToString();
        }

        private TagBuilder AddEditLink(T item, MultiSelectTableHelperField<T> field, int index)
        {
            var urlHelper = new UrlHelper(this.htmlHelper.ViewContext);
            var parameters = GetEditParametersOfField(item, field);
            var url = string.IsNullOrEmpty(field.Link.Controller)
                          ? urlHelper.Action(field.Link.Action, parameters)
                          : urlHelper.Action(field.Link.Action, field.Link.Controller, parameters);
            var anchor = new TagBuilder("a");
            anchor.Attributes.Add("href", url);
            var linkTemplate = ((MultiselectTableHelperLink<T>)field.Link).Template;
            if (linkTemplate != null)
            {
                var tableValue = CreateTableValueForField(item, field, index);
                if (linkTemplate.Delegate.DynamicInvoke(tableValue) is HelperResult innerAnchor)
                {
                    anchor.InnerHtml.AppendHtml(innerAnchor);
                    //anchor.InnerHtml = innerAnchor.ToHtmlString();
                }
            }
            else
            {

                anchor.InnerHtml.Append(field.GetValueText(item));
            }

            return anchor;
        }

        /// <summary>
        /// Generates the [thead] tag of the table and its contents
        /// </summary>
        /// <returns>Html code of the [thead] tag</returns>
        private TagBuilder AddHead()
        {
            var writer = new TagBuilder("thead");
            var content = new TagBuilder("tr");
            if (!string.IsNullOrEmpty(TableHeaderCssClass))
            {
                writer.Attributes.Add("class", $"{TableHeaderCssClass}");
            }
            var fieldsToDisplay = GetFieldsToDisplay();
            if (this.ShowHeader)
            {
                if (ShowSelectColumn)
                {
                    content.InnerHtml.AppendHtml(AddSelectHeader());
                }

                foreach (var field in fieldsToDisplay)
                {
                    content.InnerHtml.AppendHtml(AddItemValueColumnHeader(field));
                }

                if (ShowEditLink)
                {
                    content.InnerHtml.Append(AddEditHeader());
                }
            }
            writer.InnerHtml.AppendHtml(content);
            //writer.InnerHtml = innerHtml.ToString();
            return writer;
        }

        /// <summary>
        /// Generates the [tr] tag and its contents for one item
        /// </summary>
        /// <param name="item">Item with row data</param>
        /// <returns>Html content of the [tr] tag</returns>
        private TagBuilder AddItemRow(T item)
        {
            var writer = new TagBuilder("tr");
            //var innerHtml = new StringBuilder();
            if (ShowSelectColumn)
            {
                writer.InnerHtml.AppendHtml(AddSelectColumn(item));
            }

            var colidx = 0;
            foreach (var field in GetFieldsToDisplay())
            {
                writer.InnerHtml.AppendHtml(AddItemValueColumn(item, field, colidx++));
            }

            if (ShowEditLink)
            {
                writer.InnerHtml.AppendHtml(AddEditColumn(item));
            }

            //writer.InnerHtml.Append(innerHtml.ToString());
            //writer.InnerHtml = innerHtml.ToString();
            return writer;
        }

        /// <summary>
        /// Generates the [td] tag for a property value of the item
        /// </summary>
        /// <param name="item">Item being generated</param>
        /// <param name="field">Field being rendered</param>
        /// <param name="index">Column index</param>
        /// <returns>Html code of the [td] tag</returns>
        private TagBuilder AddItemValueColumn(T item, MultiSelectTableHelperField<T> field, int index)
        {
            var template = field.Template;
            TagBuilder content;
            var td = new TagBuilder("td");

            if (template != null)
            {
                content = AddTemplatedItemValueColumn(item, field, index, template);
            }
            else
            {
                content = DefaultColumnTemplate != null
                              ? AddDefaultTemplateItemValueColumn(item, field, index)
                              : AddNonTemplateItemValueColumn(item, field, index);
            }

            td.InnerHtml.AppendHtml(content);
            return td;
        }

        /// <summary>
        /// Generates the header for a property
        /// </summary>
        /// <param name="field">Property to generate header</param>
        /// <returns>Html code of the select header</returns>
        private TagBuilder AddItemValueColumnHeader(MultiSelectTableHelperField<T> field)
        {
            var isSortable = AllowSorting && field.IsSortable;
            return isSortable
                       ? AddSortableItemValueColumnHeader(field)
                       : AddNonSortableItemValueColumnHeader(field);
        }

        private TagBuilder AddNonSortableItemValueColumnHeader(MultiSelectTableHelperField<T> field)
        {
            var text = field.GetHeaderText();
            var writer = new TagBuilder("th");
            writer.InnerHtml.Append(text);
            return writer;
        }

        private TagBuilder AddNonTemplateItemValueColumn(T item, MultiSelectTableHelperField<T> field, int index)
        {
            var tag = new TagBuilder("span");
            var valueField = field.GetValueText(item);
            PropertyInfo property = item.GetType().GetProperty(field.Name);
            
            if (property != null && property.PropertyType == typeof(bool))
            {
                valueField = Translation.GetTranslationText(valueField);
            }
            tag.InnerHtml.Append(valueField);
            return field.Link.HasLink
                       ? AddEditLink(item, field, index)
                       : tag;
        }

        /// <summary>
        /// Generates the select column of a item
        /// </summary>
        /// <param name="item">Item being generated</param>
        /// <returns>Html code of the select column</returns>
        private TagBuilder AddSelectColumn(T item)
        {
            var root = new TagBuilder("td");
            var id = (TK)keyProperty.GetValue(item);
            var writer = GetCheckboxBuilder("SelectedIds", id.ToString(), this.data.SelectedIds.Contains(id));
            writer.Attributes.Add("data-select-all", "false");
            var writerLabel = GetCheckboxLabelBuilder("SelectedIds" + id.ToString());
            root.InnerHtml.AppendHtml(writer);
            //root.InnerHtml = writer.ToString();
            root.InnerHtml.AppendHtml(writerLabel);
            return root;
        }

        /// <summary>
        /// Generates the select header
        /// </summary>
        /// <returns>Html code of the select header</returns>
        private TagBuilder AddSelectHeader()
        {
            var writer = new TagBuilder("th");
            var guid = Guid.NewGuid().ToString();
            var check = GetEmptyCheckboxBuilder();
            check.Attributes.Add("id", guid);
            check.Attributes.Add("data-select-all", "true");
            check.TagRenderMode = TagRenderMode.SelfClosing;
            var label = GetCheckboxLabelBuilder(guid);
            label.TagRenderMode = TagRenderMode.SelfClosing;
            writer.InnerHtml.AppendHtml(check);
            writer.InnerHtml.AppendHtml(label);
            return writer;
        }

        private TagBuilder AddSortableItemValueColumnHeader(MultiSelectTableHelperField<T> field)
        {
            var text = field.GetHeaderText();
            var th = new TagBuilder("th");
            var anchor = new TagBuilder("a");
            var sortIcon = new TagBuilder("i");
            anchor.InnerHtml.Append(text);
            anchor.Attributes.Add("href", "#");
            anchor.Attributes.Add("data-order", field.Name);
            anchor.AddCssClass("table-header");
            //if (field.IsSortable)
            //{
            //    th.Attributes.Add("class", "fa fa-sort");
            //}
            if (Filter != null && Filter.OrderBy == field.Name)
            {
                anchor.AddCssClass("table-header-ordered");
                var span = new TagBuilder("span");
                span.AddCssClass("fa fa-sort");
                var cssFilter = Filter.Asc ? "fa-fw fa-sort" : "fa-fw fa-sort-asc";
                span.AddCssClass(cssFilter);
                th.InnerHtml.AppendHtml(anchor);
                th.InnerHtml.AppendHtml(span);
                //th.InnerHtml.Append(string.Concat(span.ToString(), anchor.ToString()));
                //th.InnerHtml = string.Concat(span.ToString(), anchor.ToString());
            }
            else
            {
                th.InnerHtml.AppendHtml(anchor);
                //th.InnerHtml = anchor.ToString();
            }

            return th;
        }

        private TagBuilder AddTemplatedItemValueColumn(
            T item,
            MultiSelectTableHelperField<T> field,
            int index,
            TemplateInfo template)
        {
            TagBuilder linkHtml = field.Link.HasLink ? AddEditLink(item, field, index) : new TagBuilder("span");
            var tableValue = CreateTableValueForField(item, field, index);
            tableValue.LinkHtml = linkHtml;
            HelperResult result = null;
            if (tableValue.RawValue != null)
            {
                result = template.Delegate.DynamicInvoke(tableValue) as HelperResult;
            }
            var tagBuilder = new TagBuilder("span");
            if (result == null)
            {
                tagBuilder.InnerHtml.Append(template.NullValue);
            }
            else
            {
                tagBuilder.InnerHtml.AppendHtml(result);
            }

            return tagBuilder;
        }

        private TagBuilder GetCheckboxBuilder(string name, string value, bool isChecked)
        {
            var writer = this.GetEmptyCheckboxBuilder();
            writer.Attributes.Add("name", name);
            writer.Attributes.Add("value", value);
            writer.Attributes.Add("id", name + value);
            if (isChecked)
            {
                writer.Attributes.Add("checked", "checked");
            }

            return writer;
        }

        private TagBuilder GetCheckboxLabelBuilder(string forAttribute)
        {
            var writerLabel = new TagBuilder("label");
            writerLabel.Attributes.Add("for", forAttribute);
            writerLabel.Attributes.Add("class", "css-label");
            return writerLabel;
        }

        private object GetEditParametersOfField(T item, MultiSelectTableHelperField<T> field)
        {
            return field.Link.ParameterFunc == null
                       ? new { id = keyProperty.GetValue(item).ToString() }
                       : field.Link.ParameterFunc(item);
        }

        private TagBuilder GetEmptyCheckboxBuilder()
        {
            var writer = new TagBuilder("input");
            writer.Attributes.Add("type", "checkbox");
            writer.Attributes.Add("class", "css-checkbox");
            return writer;
        }

        private MultiSelectTableHelperField<T> GetField(string name)
        {
            return fields.ContainsKey(name) ? fields[name] : null;
        }

        private IEnumerable<MultiSelectTableHelperField<T>> GetFieldsToDisplay()
        {
            if (!fields.Any())
            {
                return
                    typeof(T).GetProperties()
                        .Where(p => p.CanRead)
                        .Select(
                            p =>
                            new MultiSelectTableHelperFieldProperty<T>(
                                p,
                                Translation,
                                UseKeyPropertyLikeAEditLink && p == keyProperty));
            }

            return fields.Values.Where(p => p.CanDisplay);
        }

        private MultiSelectTableHelperField<T> GetOrCreateField(PropertyInfo propertyInfo, string headerText = null)
        {
            var key = propertyInfo.Name;
            if (fields.ContainsKey(key))
            {
                return fields[key];
            }

            var field = new MultiSelectTableHelperFieldProperty<T>(propertyInfo, Translation);
            field.HeaderText = headerText;
            if (UseKeyPropertyLikeAEditLink && propertyInfo == keyProperty)
            {
                field.Link.HasLink = true;
            }

            fields.Add(key, field);
            return field;
        }

        private class MultiSelectTableHelperField<TRoot> : IMultiselectTableHelperField<TRoot>
            where TRoot : class
        {
            private readonly MultiselectTableHelperLink<TRoot> link;
            public MultiSelectTableHelperField(string name, bool hasLink = false)
            {
                Name = name;
                IsSortable = true;
                link = new MultiselectTableHelperLink<TRoot> { HasLink = hasLink };
            }

            public virtual bool CanDisplay => true;

            public bool IsSortable { get; set; }

            public IMultiselectTableHelperLink<TRoot> Link => link;

            public string Name { get; }

            public TemplateInfo Template { get; set; }

            public int Width { get; set; }

            public virtual TableColumnValue CreateTableValueObject()
            {
                return new TableColumnValue<string>();
            }

            public virtual string GetHeaderText()
            {
                return Name;
            }

            public virtual object GetRawValue(TRoot item)
            {
                return Name;
            }

            public virtual string GetValueText(TRoot item)
            {
                return Name;
            }

            public void SetTemplate(Func<TableColumnValue, HelperResult> templateDelegate)
            {
                SetTemplate(templateDelegate, string.Empty);
            }

            public void SetTemplate(Func<TableColumnValue, HelperResult> templateDelegate, string nullValue)
            {
                Template = new TemplateInfo { Delegate = templateDelegate, NullValue = nullValue };
            }

            public void SetTemplate<TP>(Func<TableColumnValue<TP>, HelperResult> templateDelegate)
            {
                SetTemplate(templateDelegate, string.Empty);
            }

            public void SetTemplate<TP>(Func<TableColumnValue<TP>, HelperResult> templateDelegate, string nullValue)
            {
                Template = new TemplateInfo { Delegate = templateDelegate, NullValue = nullValue };
            }
        }

        private class MultiSelectTableHelperFieldProperty<TRoot> : MultiSelectTableHelperField<TRoot>
            where TRoot : class
        {
            private readonly PropertyInfo property;
            private readonly ITranslationTable translation;

            public MultiSelectTableHelperFieldProperty(PropertyInfo property, ITranslationTable translation, bool hasLink = false)
                : base(property.Name, hasLink)
            {
                this.property = property;
                IsSortable = true;
                this.translation = translation;
            }

            public override bool CanDisplay => property.CanRead;
            public string HeaderText { get; set; }

            public override TableColumnValue CreateTableValueObject()
            {
                var type = typeof(TableColumnValue<>);
                var genericType = type.MakeGenericType(property.PropertyType);
                var tableValue = Activator.CreateInstance(genericType) as TableColumnValue;
                return tableValue;
            }

            public override string GetHeaderText()
            {
                var textToTranslate = HeaderText ?? property.Name;
                if (translation != null && !string.IsNullOrEmpty(textToTranslate))
                {
                    var translationText = translation.GetTranslationText(textToTranslate);
                    return string.IsNullOrWhiteSpace(translationText) ? textToTranslate : translationText;
                }
                else
                {
                    return textToTranslate;
                }
            }

            public override object GetRawValue(TRoot item)
            {
                return property.GetValue(item);
            }

            public override string GetValueText(TRoot item)
            {
                return property.GetSafeStringValue(item);
            }
        }

        private class MultiSelectTableHelperFieldStatic<TRoot> : MultiSelectTableHelperField<TRoot>,
                                                                 IMultiselectTableHelperFieldStatic<TRoot>
            where TRoot : class
        {
            public MultiSelectTableHelperFieldStatic(string name)
                : base(name)
            {
            }

            public string HeaderText { get; set; }

            public string Text { get; set; }

            public override string GetHeaderText()
            {
                return HeaderText ?? Text;
            }

            public override object GetRawValue(TRoot item)
            {
                return Text;
            }

            public override string GetValueText(TRoot item)
            {
                return Text;
            }
        }

        private class MultiselectTableHelperLink<TLink> : IMultiselectTableHelperLink<TLink>
            where TLink : class
        {
            public MultiselectTableHelperLink()
            {
                HasLink = false;
                Action = "Edit";
                Controller = null;
                ParameterFunc = null;
            }

            public string Action { get; set; }

            public string Controller { get; set; }

            public bool HasLink { get; set; }

            public Func<TLink, object> ParameterFunc { get; set; }

            public TemplateInfo Template { get; private set; }

            public void SetLinkTemplate<TP>(Func<TableColumnValue<TP>, HelperResult> linkDelegate, string nullValue)
            {
                Template = new TemplateInfo { Delegate = linkDelegate, NullValue = nullValue };
            }
        }

        private class TemplateInfo
        {
            public Delegate Delegate { get; set; }

            public string NullValue { get; set; }
        }
    }
}
