using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Bill
{
    public class BillDetailViewModel
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public double Units { get; set; }
        public string SerialNumber { get; set; }
    }
}