namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes
{
    public class WorkOrderTypeExtendedDto : WorkOrderTypeDto
    {
        public WorkOrderTypeDto Parent { get; set; }
    }
}