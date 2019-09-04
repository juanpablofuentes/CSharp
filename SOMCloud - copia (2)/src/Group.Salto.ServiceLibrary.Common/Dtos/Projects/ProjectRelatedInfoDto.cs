using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Projects
{
    public class ProjectRelatedInfoDto
    {
        public int Id { get; set; }
        public int QueueId { get; set; }
        public int WorkOrderStatusId { get; set; }
        public IList<BaseNameIdDto<int>> WorkOrderCategories { get; set; }
        public List<WorkOrderTypeFatherDto> WorkOrderTypes { get; set; }
    }
}