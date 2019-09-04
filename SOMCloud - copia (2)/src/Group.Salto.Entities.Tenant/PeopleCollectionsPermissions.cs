namespace Group.Salto.Entities.Tenant
{
    public class PeopleCollectionsPermissions
    {
        public int PermissionId { get; set; }
        public int PeopleCollectionId { get; set; }

        public PeopleCollections PeopleCollection { get; set; }
        public Permissions Permissions { get; set; }
    }
}
