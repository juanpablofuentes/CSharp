using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.RepetitionParameter
{
    public class RepetitionParameterViewModel
    {
        public Guid Id { get; set; }
        public int Days { get; set; }
        public string DaysType { get; set; }
        public string DamagedEquipment { get; set; }
        public string CalculationType { get; set; }
    }
}