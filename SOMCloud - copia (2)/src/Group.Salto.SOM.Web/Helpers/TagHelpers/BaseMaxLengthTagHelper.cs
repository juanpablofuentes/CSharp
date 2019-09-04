using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Helpers.TagHelpers
{
     public class BaseMaxLengthTagHelper : TagHelper
    {
        public override int Order { get; } = int.MaxValue;

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (context.AllAttributes["maxlength"] == null)
            {
                int maxLength = GetMaxLength(For.ModelExplorer.Metadata.ValidatorMetadata);
                if (maxLength > 0)
                {
                    output.Attributes.Add("maxlength", maxLength);
                    output.Attributes.RemoveAll("data-val-maxlength");
                    output.Attributes.RemoveAll("data-val-maxlength-max");
                }
            }
        }

        private int GetMaxLength(IReadOnlyList<object> validatorMetadata)
        {
            for (int i = 0; i < validatorMetadata.Count; i++)
            {
                if (validatorMetadata[i] is StringLengthAttribute stringLengthAttribute && stringLengthAttribute.MaximumLength > 0)
                {
                    return stringLengthAttribute.MaximumLength;
                }

                if (validatorMetadata[i] is MaxLengthAttribute maxLengthAttribute && maxLengthAttribute.Length > 0)
                {
                    return maxLengthAttribute.Length;
                }
            }
            return 0;
        }
    }
}