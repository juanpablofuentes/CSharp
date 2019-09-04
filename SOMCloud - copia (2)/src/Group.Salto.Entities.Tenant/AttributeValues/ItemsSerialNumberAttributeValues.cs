namespace Group.Salto.Entities.Tenant.AttributedEntities
{
    public class ItemsSerialNumberAttributeValues : BaseAttributeValues
    {
        public int ItemId { get; set; }
        public string SerialNumber { get; set; }
        public ItemsSerialNumber ItemsSerialNumber { get; set; }
    }
}