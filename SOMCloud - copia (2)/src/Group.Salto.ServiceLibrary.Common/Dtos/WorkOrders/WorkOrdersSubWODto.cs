namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrdersSubWODto
    {
        public int Id { get; set; }
        public string ResolutionDateSla { get; set; }
        public string TimingCreation { get; set; }
        public string InternalIdentifier { get; set; }
        public string WorkOrderStatus { get; set; }
        public string ActionDate { get; set; }
        public string PeopleResponsible { get; set; }
        public string WorkOrderCategory { get; set; }
        public string Project { get; set; }
        public string Queue { get; set; }
        public string TimingAssigned { get; set; }
    }
}