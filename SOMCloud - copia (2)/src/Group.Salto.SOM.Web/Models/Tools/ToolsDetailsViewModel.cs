using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Tools
{
    public class ToolsDetailsViewModel : ToolViewModel
    {
        public IEnumerable<SelectListItem> Vehicles { get; set; }
        public IList<MultiComboViewModel<int, int>> Types { get; set; }

    }
}
