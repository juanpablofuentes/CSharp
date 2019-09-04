namespace Group.Salto.Entities.Tenant
{
    public class AssetsAuditChanges
    {
        public int AssetAuditId { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public AssetsAudit AssetAudit { get; set; }
    }
}
