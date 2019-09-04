using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter
{
    public class RepetitionParameterDto
    {
        public Guid Id { get; set; }
        public int Days { get; set; }
        public Guid IdDaysType { get; set; }
        public Guid IdDamagedEquipment { get; set; }
        public Guid IdCalculationType { get; set; }
    }
}
