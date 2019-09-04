using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Bill
{
    public class BillsViewModel
    {
        public MultiItemViewModel<BillViewModel, int> BillViewModels { get; set; }
        public BillViewModel Bill { get; set; }
        public MultiItemViewModel<BillDetailViewModel, int> BillItems { get; set; }
        public BillFilterViewModel BillFilter { get; set; }
    }
}