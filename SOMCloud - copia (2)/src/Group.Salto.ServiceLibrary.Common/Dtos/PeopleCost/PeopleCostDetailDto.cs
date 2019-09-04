using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.PeopleCost
{
    public class PeopleCostDetailDto
    {
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public decimal? HourCost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}