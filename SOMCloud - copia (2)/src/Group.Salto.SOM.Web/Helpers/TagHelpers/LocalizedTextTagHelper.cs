using System.Threading;
using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Helpers;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Group.Salto.SOM.Web.Helpers.TagHelpers
{
    [HtmlTargetElement(Attributes = MessagevalueAttributeName)]
    public class LocalizedTextTagHelper : TagHelper
    {
        private const string MessagevalueAttributeName = AppConstants.LocalizedPropertyName;
        private const string StringFormatAttibuteName = AppConstants.StringFormatProperyName;
        
        [HtmlAttributeName(MessagevalueAttributeName)]
        public string MessageValue { get; set; }

        [HtmlAttributeName(StringFormatAttibuteName)]
        public string StringFormat { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var culture = ViewContext.HttpContext.Request.GetCookie(AppConstants.CookieLanguageConstant);
            culture = !string.IsNullOrEmpty(culture)
                ? culture
                : Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetContent(GetText(MessageValue,culture));
            base.Process(context, output);
        }

        public string GetText(string key, string culture)
        {
            var result = TranslationHelper.GetText(key, culture);
            if (!string.IsNullOrEmpty(StringFormat))
            {
                result = string.Format(StringFormat, result);
            }

            return result;
        }
    }
}
