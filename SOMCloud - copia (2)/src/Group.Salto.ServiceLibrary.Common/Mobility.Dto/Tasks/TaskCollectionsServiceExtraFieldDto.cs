using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks
{
    public class TaskCollectionsServiceExtraFieldDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public IEnumerable<ExtendedExtraFieldValueDto> ExtraFieldValues { get; set; }
    }
}
