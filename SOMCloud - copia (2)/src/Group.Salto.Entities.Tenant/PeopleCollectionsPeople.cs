namespace Group.Salto.Entities.Tenant
{
    public class PeopleCollectionsPeople
    {
        public int PeopleCollectionId { get; set; }
        public int PeopleId { get; set; }

        public People People { get; set; }
        public PeopleCollections PeopleCollection { get; set; }
    }
}
