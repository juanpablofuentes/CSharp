using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.PurchaseRate
{
    public class PurchaseRateDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RefenrenceErp { get; set; }
       
    }
}