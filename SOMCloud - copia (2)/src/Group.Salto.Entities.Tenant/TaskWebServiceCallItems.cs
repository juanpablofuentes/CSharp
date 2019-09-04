namespace Group.Salto.Entities.Tenant
{
    public class TaskWebServiceCallItems
    {
        public int TaskId { get; set; }
        public int ItemId { get; set; }
        public string Value { get; set; }

        public Tasks Task { get; set; }
    }
}
