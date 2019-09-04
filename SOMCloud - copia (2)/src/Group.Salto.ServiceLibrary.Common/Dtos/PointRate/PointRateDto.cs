using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.PointRate
{
    public class PointRateDto
    {
        public int Id { get; set; }
        public string ErpReference { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
