using Group.Salto.SOM.Web.Models.MultiCombo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Assets
{
    public class AssetsTransferViewModel
    {
        public int[] AssetsId { get; set; }
        public int? AssetsStatusId { get; set; }
        public MultiComboViewModel<int, string> User { get; set; }
        public MultiComboViewModel<int, string> Location { get; set; }
        public int? PageClientId { get; set; }
        public int? PageSiteId { get; set; }
        public int UserId { get; set; }
    }
}