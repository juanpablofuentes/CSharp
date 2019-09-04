namespace Group.Salto.SOM.Web.Models.Items
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ERPReference { get; set; }
        public string InternalReference { get; set; }
        public int ItemsTypeId { get; set; }
        public string ItemsTypeName { get; set; }
        public bool SyncErp { get; set; }
        public bool IsBlocked { get; set; }
    }
}