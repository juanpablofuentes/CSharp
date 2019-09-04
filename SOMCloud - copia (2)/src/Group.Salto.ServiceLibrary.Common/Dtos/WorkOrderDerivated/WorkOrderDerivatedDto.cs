using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderDerivated
{
    public class WorkOrderDerivatedDto : WorkOrderEditDto
    {
        public int TaskId { get; set; }
        public int? PeopleManipulatorId { get; set; }
        public bool InheritProject { get; set; }
        public bool InheritTechnician { get; set; }
        public int GeneratorServiceDuplicationPolicy { get; set; }
        public int OtherServicesDuplicationPolicy { get; set; }
        public int? PeopleResponsibleTechniciansCollectionId { get; set; }
        public string QueueName { get; set; }
        public string WorkOrderStatusName { get; set; }
        public string ExternalWorOrderStatusName { get; set; }
        public string WorkOrderTypeName { get; set; }
        public string WorkOrderCategoryName { get; set; }
    }
}