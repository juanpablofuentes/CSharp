namespace Group.Salto.Entities.Tenant
{
    public class MainWoviewConfigurationsGroups
    {
        public int UserMainWoviewConfigurationId { get; set; }
        public int PeopleCollectionId { get; set; }

        public PeopleCollections PeopleCollection { get; set; }
        public UsersMainWoviewConfigurations UserMainWoviewConfiguration { get; set; }
    }
}
