using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.SiteUser
{
    public class SiteUserDetailViewModel : SiteUserViewModel
    {
        public string Position { get; set; }
        public int LocationId { get; set; }
    }
}
