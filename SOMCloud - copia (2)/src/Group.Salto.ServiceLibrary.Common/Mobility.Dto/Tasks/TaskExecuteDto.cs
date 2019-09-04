using System;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks
{
    public class TaskExecuteDto
    {
        public int Id { get; set; }
        public int ResponsibleId { get; set; }
        public TaskTypeEnum Type { get; set; }
        public int WorkOrderId { get; set; }
        public string Observations { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ServiceExecuteDto Service { get; set; }
        public DateTime Date { get; set; }
        public TechnicianDateDto TechnicianAndActuationDate { get; set; }
    }
}
