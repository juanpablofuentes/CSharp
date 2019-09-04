using Group.Salto.SOM.Web.Models.MultiCombo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.ServiceGauges
{
    public class ServiceGaugesFilterViewModel : BaseFilter
    {
        public int? WoId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public MultiComboViewModel<int?, string> Project { get; set; }
        public MultiComboViewModel<int?, string> Client { get; set; }
        public float NumVisits { get; set; }
        public int Clientint { get; set; }
        public int Projectint { get; set; }
        public int WoCategory { get; set; }
    }
}