using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ServiceGauges
{
    public class ServiceGaugesFilterDto : BaseFilterDto
    {
        public int? WoId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string  Project { get; set; }
        public string Client { get; set; }
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public int WoCategory { get; set; }
    }
}