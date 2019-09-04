namespace Group.Salto.Entities.Tenant
{
    public class PermissionsQueues
    {
        public int PermissionId { get; set; }
        public int QueueId { get; set; }

        public Queues Queue { get; set; }
        public Permissions Permission { get; set; }
    }
}
