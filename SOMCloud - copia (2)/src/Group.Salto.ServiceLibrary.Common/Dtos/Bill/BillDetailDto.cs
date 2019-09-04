using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Bill
{
    public class BillDetailDto
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public double Units { get; set; }
        public string SerialNumber { get; set; }
    }
}