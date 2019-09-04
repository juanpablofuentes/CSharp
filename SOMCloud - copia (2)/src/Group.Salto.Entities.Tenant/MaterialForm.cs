using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class MaterialForm : BaseEntity
    {
        public int ExtraFieldValueId { get; set; }
        public string SerialNumber { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public int? Units { get; set; }
        public int? AssetId { get; set; }

        public ExtraFieldsValues ExtraFieldValue { get; set; }
        public Assets Asset { get; set; }
    }
}
