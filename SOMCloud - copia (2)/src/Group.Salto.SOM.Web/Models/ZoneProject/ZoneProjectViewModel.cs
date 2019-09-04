using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.ServiceLibrary.Common.Dtos.Zones;
using Group.Salto.SOM.Web.Models.Projects;
using Group.Salto.SOM.Web.Models.ZoneProjectPostalCode;
using Group.Salto.SOM.Web.Models.Zones;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.ZoneProject
{
    public class ZoneProjectViewModel
    {
        public int ZoneProjectId {get;set;}
        public int ZoneId { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public IList<SelectListItem> ProjectsList { get; set; }
        public IList<ZoneProjectViewModel> LoadedZoneProjects { get; set; }
        public IList<ZoneProjectPostalCodeViewModel> PostalCodes { get; set; }
        public string State { get; set; }
    }
}
