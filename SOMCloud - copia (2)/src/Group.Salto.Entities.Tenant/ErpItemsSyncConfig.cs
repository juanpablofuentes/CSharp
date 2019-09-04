namespace Group.Salto.Entities.Tenant
{
    public class ErpItemsSyncConfig
    {
        public int TenantId { get; set; }
        public int ErpSystemInstanceId { get; set; }

        public ErpSystemInstance ErpSystemInstance { get; set; }
    }
}
