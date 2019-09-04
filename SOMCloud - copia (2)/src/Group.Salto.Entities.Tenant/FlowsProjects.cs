namespace Group.Salto.Entities.Tenant
{
    public class FlowsProjects
    {
        public int ProjectId { get; set; }
        public int FlowId{ get; set; }

        public Projects Projects { get; set; }
        public Flows Flows { get; set; }
    }
}