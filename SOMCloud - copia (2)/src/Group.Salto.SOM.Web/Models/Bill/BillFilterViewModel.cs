using Group.Salto.SOM.Web.Models.MultiCombo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Bill
{
    public class BillFilterViewModel : BaseFilter
    {
        public BillFilterViewModel()
        {
            OrderBy = "Id";
        }

        public int? WorkOrderId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InternalIdentifier { get; set; }
        public string ProjectSerie { get; set; }
        public MultiComboViewModel<int?, string> Project { get; set; }
        public MultiComboViewModel<int?, string> Status { get; set; }
        public MultiComboViewModel<int?, string> ItemId { get; set; }
        public int Id { get; set; }
    }
}