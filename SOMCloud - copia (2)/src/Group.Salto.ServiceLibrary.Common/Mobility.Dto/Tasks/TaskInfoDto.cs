using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks
{
    public class TaskInfoDto
    {
        public FromToPlusColorDto Changes { get; set; }
        public IEnumerable<TechnicianDto> Technicians { get; set; }
        public PredefinedServiceTaskDto Service { get; set; }
    }
}
