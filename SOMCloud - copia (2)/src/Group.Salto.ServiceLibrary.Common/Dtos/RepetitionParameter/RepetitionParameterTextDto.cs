using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter
{
    public class RepetitionParameterTextDto
    {
        public Guid Id { get; set; }
        public int Days { get; set; }
        public string DaysType { get; set; }
        public string DamagedEquipment { get; set; }
        public string CalculationType { get; set; }
    }
}
