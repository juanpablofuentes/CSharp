using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.ZoneProject
{
    public class ZoneListProjectViewModel: ZoneProjectViewModel
    {
        public IList<SelectListItem> ProjectsList { get; set; }
    }
}
