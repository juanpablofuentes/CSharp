using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes
{
    public class WorkOrderTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Serie { get; set; }
        public IList<WorkOrderTypeDto> Childs { get; set; }
        public int IdClonedItem { get; set; }
    }
}