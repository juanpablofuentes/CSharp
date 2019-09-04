namespace Group.Salto.Entities.Tenant
{
    public class MainWoViewConfigurationsPeople
    {
        public int UserMainWoViewConfigurationId { get; set; }
        public int PeopleId { get; set; }

        public People People { get; set; }
        public UsersMainWoviewConfigurations UserMainWoViewConfiguration { get; set; }
    }
}
