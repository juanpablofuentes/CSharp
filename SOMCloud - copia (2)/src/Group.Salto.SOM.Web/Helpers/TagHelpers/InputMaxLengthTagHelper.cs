using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Helpers.TagHelpers
{
    [HtmlTargetElement("input", Attributes = "asp-for")]
    public class InputMaxLengthTagHelper : BaseMaxLengthTagHelper
    {
    }
}