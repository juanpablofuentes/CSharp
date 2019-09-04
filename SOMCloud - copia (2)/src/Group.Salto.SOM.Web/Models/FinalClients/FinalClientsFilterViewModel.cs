using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Controls.Entities;

namespace Group.Salto.SOM.Web.Models.FinalClients
{
    public class FinalClientsFilterViewModel : BaseFilter
    {       
        public FinalClientsFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public bool IsDeleted { get; set; }
    }
}