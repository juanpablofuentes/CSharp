using Group.Salto.Common.Constants;
using Group.Salto.Controls.Table.Models;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Projects;
using Group.Salto.SOM.Web.Models.ZoneProject;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Zones
{    
        public class ZonesViewModel
        {
            public MultiItemViewModel<ZoneViewModel, int> Zones { get; set; }
            public ZonesFilterViewModel ZonesFilters { get; set; }
        }    
}