using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Assets
{
    public class GuaranteeViewModel
    {
        public int Id { get; set; }
        public int? IdExternal { get; set; }
        public string Standard { get; set; }
        public DateTime? StdStartDate { get; set; }
        public DateTime? StdEndDate { get; set; }
        public string Armored { get; set; }
        public DateTime? BlnStartDate { get; set; }
        public DateTime? BlnEndDate { get; set; }
        public string Provider { get; set; }
        public DateTime? ProStartDate { get; set; }
        public DateTime? ProEndDate { get; set; }
    }
}